using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using DynamicData;
using måleusikkerhet.Bases;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Models;
using måleusikkerhet.Services;
using ReactiveUI;
using Splat;

namespace måleusikkerhet.ViewModels;

public class AddDigitalDeviceFormViewModel : ViewModelBase
{
    private DigitalDeviceModel _model;
    private MeasurementType _selected;
    private ObservableCollection<DigitalAttributesModel> _currentAttributes;
    private readonly DatabaseService _database;
    public DigitalDeviceModel Model
    {
        get => _model;
        private init => this.RaiseAndSetIfChanged(ref _model, value);
    }
    
    public MeasurementType Selected
    {
        get => _selected;
        set
        {
            this.RaiseAndSetIfChanged(ref _selected, value);
            UpdateTable();
        }
    }
    
    private void UpdateTable()
    {
        SaveChanges();
        CurrentAttributes = new ObservableCollection<DigitalAttributesModel>(Model.AttributesModels.Where(x => x.Type == Selected));
    }
    
    private void SaveChanges()
    {
        var toBeRemoved = Model.AttributesModels.Where(x => x.Type == CurrentAttributes.FirstOrDefault()?.Type)
            .Except(CurrentAttributes);
        Model.AttributesModels.RemoveMany(toBeRemoved);
        Model.AttributesModels.AddRange(CurrentAttributes.Except(Model.AttributesModels));
    }
    
    public List<MeasurementType> Types => Enum.GetValues<MeasurementType>().ToList();
    
    public ObservableCollection<DigitalAttributesModel> CurrentAttributes
    {
        get => _currentAttributes;
        set => this.RaiseAndSetIfChanged(ref _currentAttributes, value);
    }
    public ReactiveCommand<Unit,Unit> Add { get; }
    public ReactiveCommand<DigitalAttributesModel,Unit> Delete { get; }
    public ReactiveCommand<Unit,Unit> SaveCommand { get; }

    public AddDigitalDeviceFormViewModel()
    {
        var currentDevice = Locator.Current.GetService<CurrentDeviceService>().DigitalDeviceModel;
        _database = Locator.Current.GetService<DatabaseService>();
        _model = currentDevice ?? new DigitalDeviceModel();
        _currentAttributes =
            new ObservableCollection<DigitalAttributesModel>(Model.AttributesModels.Where(x => x.Type == Selected));
        Add = ReactiveCommand.Create(AddAttribute);
        Delete = ReactiveCommand.Create<DigitalAttributesModel>(Remove);
        SaveCommand = ReactiveCommand.Create(Save);
    }
    private void Remove(DigitalAttributesModel model)
    {
        CurrentAttributes.Remove(model);
    }

    private void AddAttribute()
    {
        CurrentAttributes.Add(new DigitalAttributesModel(Selected));
    }


    public async void LoadImage()
    {
        var fileDialog = new OpenFileDialog {AllowMultiple = false};
        var file = await fileDialog.ShowAsync(new Window());
        if(file?[0] == null)return;
        var bitmap = new Bitmap(file[0]);
        bitmap = bitmap.CreateScaledBitmap(new PixelSize(128, 128));
        Model.Image = bitmap;
    }

    public void Save()
    { 
        //Todo: Validate and Close
        SaveChanges();
        using var ms = new MemoryStream();
        Model.Image?.Save(ms);
        var device = new Digital(Model.Name)
        {
            Image = new ImageDb { ImageData = ms.ToArray() },
            Ranges = Model.AttributesModels.Select(DigitalAtributesToModel.FromModel).ToList()
        };
        _database.AddDevice(device);
    }
}
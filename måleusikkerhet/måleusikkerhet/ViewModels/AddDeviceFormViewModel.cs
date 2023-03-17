using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using måleusikkerhet.Bases;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Services;
using ReactiveUI;
using Splat;
using MemoryStream = System.IO.MemoryStream;

namespace måleusikkerhet.ViewModels;

public class AddDeviceFormViewModel : ViewModelBase
{
    private Bitmap? _image;
    private ObservableCollection<DigitalAttributes> _attributesList = new();
    private readonly DatabaseService _database;

    public Bitmap? Image
    {
        get => _image;
        set => this.RaiseAndSetIfChanged(ref _image, value);
    }

    public static List<string> SupportedTypes => Enum.GetNames(typeof(MeasurementType)).ToList();

    public ObservableCollection<DigitalAttributes> AttributesList
    {
        get => _attributesList;
        set => this.RaiseAndSetIfChanged(ref _attributesList, value);
    }

    public double Resolution { get; set; }
    public string? Name { get; set; }
    public MeasurementType SelectedItem { get; set; }

    public AddDeviceFormViewModel()
    {
        _database = Locator.Current.GetService<DatabaseService>();
        AttributesList.Add(new DigitalAttributes());
    }
    

    public async void LoadImage()
    {
        var fileDialog = new OpenFileDialog {AllowMultiple = false};
        var file = await fileDialog.ShowAsync(new Window());
        if(file?[0] == null)return;
        var bitmap = new Bitmap(file[0]);
        bitmap = bitmap.CreateScaledBitmap(new PixelSize(128, 128));
        Image = bitmap;
    }

    public void Save()
    {
        if (Name == null || Image == null) return;
        using var ms = new MemoryStream();
        Image.Save(ms);
        var newDevice = new Digital(Name)
        {
            Image = new ImageDb { ImageData = ms.ToArray() }, Resolution = Resolution, MeasumentType = SelectedItem,
            Ranges = AttributesList.OrderBy(x => x.Range).ToList()
        };
        _database.AddDevice(newDevice);
    }
}
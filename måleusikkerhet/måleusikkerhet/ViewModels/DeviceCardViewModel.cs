using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using måleusikkerhet.Models;
using måleusikkerhet.Services;
using måleusikkerhet.Views;
using ReactiveUI;
using Splat;

namespace måleusikkerhet.ViewModels;

public class DeviceCardViewModel : ViewModelBase
{
    private readonly DatabaseService _database;
    private readonly CurrentDeviceService _currentDevice;

    public DeviceCardViewModel(string name, Bitmap image)
    {
        _database = Locator.Current.GetService<DatabaseService>();
        _currentDevice = Locator.Current.GetService<CurrentDeviceService>();
        Name = name;
        Image = image;
        Edit = ReactiveCommand.CreateFromTask(EditTask);
    }

    private async Task EditTask()
    {
        var device = _database.GetDeviceModel(Name);
        switch (device)
        {
            case PreciseDeviceModel preciseDeviceModel:
                _currentDevice.PreciseDeviceModel = preciseDeviceModel;
                await Dispatcher.UIThread.InvokeAsync(new AddPreciseDeviceForm().Show);
                break;
            case DigitalDeviceModel digitalDeviceModel:
                _currentDevice.DigitalDeviceModel = digitalDeviceModel;
                await Dispatcher.UIThread.InvokeAsync(new AddDigitalDeviceForm().Show);
                break;
            case AnalogDeviceModel analogDeviceModel:
                _currentDevice.AnalogDeviceModel = analogDeviceModel;
                await Dispatcher.UIThread.InvokeAsync(new AddAnalogDeviceForm().Show);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(device));
        }
    }

    public DeviceCardViewModel()    {
        _database = Locator.Current.GetService<DatabaseService>();
        _currentDevice = Locator.Current.GetService<CurrentDeviceService>();
        Edit = ReactiveCommand.CreateFromTask(EditTask);
    }

    public string? Name { get; set; }
    public Bitmap? Image { get; set; }

    public ReactiveCommand<Unit, Unit> Edit { get; }
}
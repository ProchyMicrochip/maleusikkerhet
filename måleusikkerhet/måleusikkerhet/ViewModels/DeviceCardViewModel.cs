using System;
using System.Linq;
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
    private bool _inUse;
    private readonly ActiveDeviceService _activeDevices;

    public DeviceCardViewModel(string name, Bitmap image)
    {
        _database = Locator.Current.GetService<DatabaseService>();
        _currentDevice = Locator.Current.GetService<CurrentDeviceService>();
        _activeDevices = Locator.Current.GetService<ActiveDeviceService>();
        _inUse = _activeDevices.DevBases.FirstOrDefault(x => x.Name == name) != null;
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

    public DeviceCardViewModel()
    {
        _database = Locator.Current.GetService<DatabaseService>();
        _currentDevice = Locator.Current.GetService<CurrentDeviceService>();
        _activeDevices = Locator.Current.GetService<ActiveDeviceService>();
        Edit = ReactiveCommand.CreateFromTask(EditTask);
    }

    public string? Name { get; set; }
    public Bitmap? Image { get; set; }

    public bool InUse
    {
        get => _inUse;
        set
        {
            if (value)
            {
                if (_activeDevices.DevBases.All(x => x.Name != Name))
                    _activeDevices.DevBases.Add(_database.GetDeviceBase(Name));
            }
            else
            {
                if (_activeDevices.DevBases.Any(x => x.Name == Name))
                    _activeDevices.DevBases.Remove(_activeDevices.DevBases.First(x => x.Name == Name));
            }
            this.RaiseAndSetIfChanged(ref _inUse, value);
        }
    }

    public ReactiveCommand<Unit, Unit> Edit { get; }
}
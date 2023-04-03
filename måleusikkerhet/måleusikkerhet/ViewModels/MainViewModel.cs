using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using måleusikkerhet.Services;
using måleusikkerhet.Views;
using ReactiveUI;
using Splat;

namespace måleusikkerhet.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly CurrentDeviceService _currentDeviceService;
        public ReactiveCommand<Unit, Unit> NewDigitalDevice { get; }
        public ReactiveCommand<Unit, Unit> NewAnalogDevice { get; }
        public ReactiveCommand<Unit, Unit> NewPreciseDevice { get; }
        public ReactiveCommand<Unit, Unit> OpenDevices { get; }

        public MainViewModel()
        {
            _currentDeviceService = Locator.Current.GetService<CurrentDeviceService>();
            NewDigitalDevice = ReactiveCommand.CreateFromTask(CreateNewDigital);
            NewAnalogDevice = ReactiveCommand.CreateFromTask(CreateNewAnalog);
            NewPreciseDevice = ReactiveCommand.CreateFromTask(CreateNewPrecise);
            OpenDevices = ReactiveCommand.CreateFromTask(Devices);
        }

        private async Task Devices()
        {
            await Dispatcher.UIThread.InvokeAsync(() => new DeviceView().Show());
        }

        private async Task CreateNewDigital()
        {
            _currentDeviceService.DigitalDeviceModel = null;
            await Dispatcher.UIThread.InvokeAsync(() => new AddDigitalDeviceForm().Show());
        }

        private async Task CreateNewAnalog()
        {
            _currentDeviceService.AnalogDeviceModel = null;
            await Dispatcher.UIThread.InvokeAsync(() => new AddAnalogDeviceForm().Show());
        }

        private async Task CreateNewPrecise()
        {
            _currentDeviceService.PreciseDeviceModel = null;
            await Dispatcher.UIThread.InvokeAsync(() => new AddPreciseDeviceForm().Show());
        }
    }
}
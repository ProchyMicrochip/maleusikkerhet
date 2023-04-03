﻿using System;
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
        public ReactiveCommand<Unit, Unit> NewDigitalDevice { get; }
        public ReactiveCommand<Unit, Unit> NewAnalogDevice { get; }
        public ReactiveCommand<Unit, Unit> NewPreciseDevice { get; }
        public ReactiveCommand<Unit, Unit> OpenDevices { get; }

        public MainViewModel()
        {
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
            await Dispatcher.UIThread.InvokeAsync(() => new AddDigitalDeviceForm().Show());
        }

        private async Task CreateNewAnalog()
        {
            throw new NotImplementedException();
        }

        private async Task CreateNewPrecise()
        {
            await Dispatcher.UIThread.InvokeAsync(() => new AddPreciseDeviceForm().Show());
        }
    }
}
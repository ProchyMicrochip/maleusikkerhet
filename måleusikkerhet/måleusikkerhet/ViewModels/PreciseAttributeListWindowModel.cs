using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls.Templates;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Models;
using måleusikkerhet.Services;
using ReactiveUI;
using Splat;

namespace måleusikkerhet.ViewModels;

public class AttributeListWindowModel : ViewModelBase
{
    public MeasurementType MeasurementType { get; init; }

    public string Unit => MeasurementType switch
    {
        MeasurementType.VoltageDc => "V",
        MeasurementType.VoltageRms => "V",
        MeasurementType.CurrentDc => "A",
        MeasurementType.CurrentRms => "A",
        MeasurementType.Frequency => "Hz",
        MeasurementType.Resistance => "Ω",
        _ => throw new ArgumentOutOfRangeException()
    };

    private ObservableCollection<PreciseAttributesModel>? _smartCollection;
    private readonly CurrentDeviceService? _modelService;

    public ObservableCollection<PreciseAttributesModel>? SmartCollection
    {
        get => _smartCollection;
        set => this.RaiseAndSetIfChanged(ref _smartCollection, value);
    }

    private ReactiveCommand<Unit, Unit> Click { get; set; }

    public AttributeListWindowModel()
    {

    }

    public void Save()
    {

    }
    
    public delegate void InvalidatedHandler();

    public delegate void CloseHandler();

    public event InvalidatedHandler? Invalidate;

    public event CloseHandler? OnClose;

        private void ModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender as PreciseAttributesModel is
            { RangeError: null, MeasureError: null, Range: null, Frequency: null }) return;
        var result = false;
        switch (e.PropertyName)
        {
            case nameof(PreciseAttributesModel.Range):
                if (sender as PreciseAttributesModel is { RangeError: null, Frequency: null, MeasureError: null })
                    result = true;
                break;
            case nameof(PreciseAttributesModel.RangeError):
                if (sender as PreciseAttributesModel is { Range: null, Frequency: null, MeasureError: null })
                    result = true;
                break;
            case nameof(PreciseAttributesModel.Frequency):
                if (sender as PreciseAttributesModel is { RangeError: null, Range: null, MeasureError: null })
                    result = true;
                break;
            case nameof(PreciseAttributesModel.MeasureError):
                if (sender as PreciseAttributesModel is { RangeError: null, Frequency: null, Range: null })
                    result = true;
                break;
        }

        if (!result) return;
        var model = new PreciseAttributesModel(MeasurementType.Frequency);
        model.PropertyChanged += ModelOnPropertyChanged;
        SmartCollection?.Add(model);
        Invalidate?.Invoke();
    }
}
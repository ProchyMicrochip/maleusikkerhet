using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Models;

public class DigitalAttributesModel : INotifyPropertyChanged
{
    private Precision? _measureError;
    private Precision? _range;
    private Precision? _frequency;
    private Precision? _digits;
    private Precision? _resolution;
    private MeasurementType _type;

    public DigitalAttributesModel(MeasurementType type, Precision? range = null, Precision? frequency = null,
        Precision? measureError = null, Precision? digits = null, Precision? resolution = null)
    {
        _type = type;
        _range = range;
        _frequency = frequency;
        _measureError = measureError;
        _digits = digits;
        _resolution = resolution;
    }

    public Precision? MeasureError
    {
        get => _measureError;
        set => SetField(ref _measureError, value);
    }

    public Precision? Range
    {
        get => _range;
        set => SetField(ref _range, value);
    }

    public Precision? Frequency
    {
        get => _frequency;
        set => SetField(ref _frequency, value);
    }

    public Precision? Digits
    {
        get => _digits;
        set => SetField(ref _digits, value);
    }

    public Precision? Resolution
    {
        get => _resolution;
        set => SetField(ref _resolution, value);
    }
    public MeasurementType Type
    {
        get => _type;
        set => SetField(ref _type, value);
    }

    #region PropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}
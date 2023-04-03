using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Models;

public class AnalogAttributesModel : INotifyPropertyChanged
{
    private Precision? _precision;
    private Precision? _range;
    private MeasurementType _measurementType;

    public AnalogAttributesModel(MeasurementType type, Precision? precision = null, Precision? range = null)
    {
        _measurementType = type;
        _precision = precision;
        _range = range;
    }

    public Precision? Precision
    {
        get => _precision;
        set => SetField(ref _precision, value);
    }

    public Precision? Range
    {
        get => _range;
        set => SetField(ref _range, value);
    }

    public MeasurementType Type
    {
        get => _measurementType;
        set => SetField(ref _measurementType, value);
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
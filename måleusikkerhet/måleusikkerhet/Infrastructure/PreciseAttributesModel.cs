using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace måleusikkerhet.Infrastructure;

public class PreciseAttributesModel : INotifyPropertyChanged
{
    private Precision? _rangeError;
    private Precision? _measureError;
    private Precision? _range;
    private Precision? _frequency;
    private MeasurementType _type;

    public PreciseAttributesModel(MeasurementType type, Precision? rangeError = null, Precision? measureError = null, Precision? range = null, Precision? frequency = null)
    {
        _rangeError = rangeError;
        _measureError = measureError;
        _range = range;
        _frequency = frequency;
        _type = type;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Precision? RangeError
    {
        get => _rangeError;
        set => SetField(ref _rangeError, value);
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

    public MeasurementType Type
    {
        get => _type;
        set => SetField(ref _type, value);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DynamicData;

namespace måleusikkerhet.Infrastructure;

public class PreciseSmartCollection : ObservableCollection<PreciseAttributesModel>
{

    
    private void Changed(object? o, PropertyChangedEventArgs a)
    {
        if (Items.Any(x => x.Frequency == Precision.Zero && x.RangeError == Precision.Zero && x.MeasureError == Precision.Zero && x.Range == Precision.Zero)) return;
        Add(new PreciseAttributesModel());
    }
}

public record struct PreciseAttributesModel(Precision? RangeError, Precision? MeasureError, Precision? Range, Precision? Frequency) : INotifyPropertyChanged
{
    private Precision? _rangeError = RangeError;
    private Precision? _measureError = MeasureError;
    private Precision? _range = Range;
    private Precision? _frequency = Frequency;
    public event PropertyChangedEventHandler? PropertyChanged;

    public Precision? RangeError
    {
        readonly get => _rangeError;
        set => SetField(ref _rangeError, value);
    }

    public Precision? MeasureError
    {
        readonly get => _measureError;
        set => SetField(ref _measureError, value);
    }

    public Precision? Range
    {
        readonly get => _range;
        set => SetField(ref _range, value);
    }

    public Precision? Frequency
    {
        readonly get => _frequency;
        set => SetField(ref _frequency, value);
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
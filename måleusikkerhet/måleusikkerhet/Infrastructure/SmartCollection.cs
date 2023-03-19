using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace måleusikkerhet.Infrastructure;

public class PreciseSmartCollection : ObservableCollection<PreciseAttributesModel>
{
    protected override void InsertItem(int index, PreciseAttributesModel item)
    {
        item.PropertyChanged += Changed;
        base.InsertItem(index, item);
    }
    
    private void Changed(object? o, PropertyChangedEventArgs a)
    {
        if (Items.Any(x => x is { Range: 0, MeasureError: 0, RangeError: 0, Frequency: 0 })) return;
        Add(new PreciseAttributesModel());
    }
}

public class PreciseAttributesModel : INotifyPropertyChanged
{
    private double _range;
    private double _measureError;
    private double _rangeError;
    private double _frequency;

    public double Range
    {
        get => _range;
        set => SetField(ref _range, value);
    }

    public double MeasureError
    {
        get => _measureError;
        set => SetField(ref _measureError, value);
    }

    public double RangeError
    {
        get => _rangeError;
        set => SetField(ref _rangeError, value);
    }

    public double Frequency
    {
        get => _frequency;
        set => SetField(ref _frequency, value);
    }

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
}
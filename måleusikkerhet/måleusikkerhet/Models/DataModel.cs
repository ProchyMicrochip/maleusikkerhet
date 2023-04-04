using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Interface;

namespace måleusikkerhet.Models;

public class DataModel : INotifyPropertyChanged
{
    private readonly IUncertainty _uncertainty;
    private readonly MeasurementType _type;
    private Precision? _value;
    private string _relative = "";

    private string _answer = "";

    //TODO: send K
    private Precision? Uncertainty => Value != null
        ? (_uncertainty.GetUncertanty(Value, Precision.Zero, _type) * new Precision(2, 0)).RoundUncertanty()
        : null;

    public DataModel(IUncertainty uncertainty, MeasurementType type)
    {
        _uncertainty = uncertainty;
        _type = type;
    }

    public Precision? Value
    {
        get => _value;
        set
        {
            SetField(ref _value, value);
            Update();
        }
    }

    public string Relative
    {
        get => _relative;
        set => SetField(ref _relative, value);
    }

    private void Update()
    {
        if (Uncertainty == null || Value == null) return;
        Relative = (Uncertainty/Value).ToPercentage();
        Answer = UncertantyRounder.ToFull(Value, Uncertainty);
    }

    public string Answer
    {
        get => _answer;
        set => SetField(ref _answer, value);
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
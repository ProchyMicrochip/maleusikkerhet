using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Analog : DevBase
{
    public List<AnalogAttributes> Ranges { get; set; } = new();

    public override Precision GetUncertanty(Precision value, Precision? frequency, MeasurementType type)
    {
        var current = Ranges.Where(x => x.MeasurementType == type).OrderBy(x => x.Range).First(x => x.Range > value);
        var device = current.Range * current.Precision * new Precision(1, -2) / new Precision(2449490, -6);
        var user = new Precision(5, -1) * current.Range / new Precision(RelativeRange, 0)/ new Precision(1732050,-6);
        var devicePow = device.Pow2();
        var userPow = user.Pow2();
        return (devicePow + userPow).Sqrt2();
    }

    public override ObservableCollection<MeasurementType> GetAvailableTypes()
    {
        return new ObservableCollection<MeasurementType>(Ranges.Select(x => x.MeasurementType).Distinct());
    }

    public Analog(string name) : base(name)
    {
    }

    public int RelativeRange { get; set; }
}
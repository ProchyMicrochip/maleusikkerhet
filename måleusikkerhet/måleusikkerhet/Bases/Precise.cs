using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Interface;

namespace måleusikkerhet.Bases;

public class Precise : DevBase
{
    public List<PreciseAttributes> Ranges { get; set; } = new();

    public override Precision GetUncertanty(Precision value, Precision? frequency, MeasurementType type)
    {
        var sortedRanges = Ranges.Where(x => x.MeasurementType == type).OrderBy(x => x.Frequency).ThenBy(x => x.Range);
        var current = frequency == null
            ? sortedRanges.First(x => x.Range > value)
            : sortedRanges.First(x => x.Range > value && x.Frequency! > value);
        return (current.MeasureError * value * new Precision(1, -2) +
                current.RangeError * current.Range * new Precision(1, -2)) / new Precision(1732050, -6);
    }
    public override ObservableCollection<MeasurementType> GetAvailableTypes()
    {
        return new ObservableCollection<MeasurementType>(Ranges.Select(x => x.MeasurementType).Distinct());
    }

    public Precise(string name) : base(name)
    {
    }
}
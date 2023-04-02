using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Precise : DevBase
{
    public List<PreciseAttributes> Ranges { get; set; } = new();

    public override Precision GetUncertanty(Precision value, Precision frequency, MeasurementType type)
    {
        var current = Ranges.Where(x => x.MeasurementType == type).OrderBy(x => x.Range)
            .First(x => x.Range > value && (x.Frequency == null || x.Frequency > frequency));
        return (current.MeasureError * value *new Precision(1,-2) + current.RangeError * current.Range *new Precision(1,-2))/new Precision(1732050,-6);
    }

    public Precise(string name) : base(name)
    {
    }
}
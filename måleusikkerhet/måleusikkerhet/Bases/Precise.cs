using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Precise : DevBase
{
    public List<PreciseAttributes> Ranges { get; set; } = new();

    public override Precision GetUncertanty(Precision value, Precision frequency)
    {
        var current = Ranges.OrderBy(x => x.Range)
            .First(x => x.Range > value && (x.Frequency == null || x.Frequency > frequency));
        return current.MeasureError * value + current.RangeError * current.Range;
    }

    public Precise(string name) : base(name)
    {
    }
}
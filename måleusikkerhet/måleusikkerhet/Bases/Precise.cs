using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Interface;

namespace måleusikkerhet.Bases;

public class Precise : DevBase
{
    public List<PreciseAttributes> Ranges { get; set; } = new();
    public override double GetUncertanty(double value, double frequency = -1)
    {
        var current = Ranges.OrderBy(x => x.Range).First(x => x.Range > value && (x.Frequency == null || x.Frequency > frequency));
        return current.MeasureError * value + current.RangeError * current.Range;
    }

    public Precise(string name) : base(name)
    {
    }
}
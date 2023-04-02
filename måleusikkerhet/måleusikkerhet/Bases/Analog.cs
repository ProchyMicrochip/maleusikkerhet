using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Analog : DevBase
{
    public List<AnalogAttributes> Ranges { get; set; } = new();
    public override Precision GetUncertanty(Precision value, Precision frequency, MeasurementType type)
    {
        var current = Ranges.Where(x => x.MeasurementType == type).OrderBy(x => x.Range).First(x => x.Range > value);
        return ((current.Range * current.Precision*new Precision(1,-2)/new Precision(3,0).Sqrt2()).Pow2() + (new Precision(5,-1) * current.Range / new Precision(RelativeRange,0)).Pow2()).Sqrt2();
    }

    public Analog(string name) : base(name)
    {
    }
    
    public int RelativeRange { get; set; }
}
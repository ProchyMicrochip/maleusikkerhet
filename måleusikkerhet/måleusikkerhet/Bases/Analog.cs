using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Analog : DevBase
{
    public List<AnalogAttributes> Ranges { get; set; } = new();
    public override Precision GetUncertanty(Precision value, Precision frequency)
    {
        var current = Ranges.OrderBy(x => x.Range).First(x => x.Range > value);
        return current.Range * current.Precision + new Precision(5,-1) * current.Range / new Precision(RelativeRange,0);
    }

    public Analog(string name) : base(name)
    {
    }
    
    public int RelativeRange { get; set; }
}
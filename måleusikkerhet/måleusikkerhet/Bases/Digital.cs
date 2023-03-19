using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Digital : DevBase
{
    
    public List<DigitalAttributes> Ranges { get; set; } = new();
    public Precision Resolution { get; set; } = new(-1, 0);

    public override Precision GetUncertanty(Precision value,Precision frequency)
    {
        var current = Ranges.OrderBy(x => x.Range).First(x => x.Range > value && (x.Frequency == null || x.Frequency > frequency));
        return value * current.RangeError + current.Digits * Resolution;
    }

    public Digital(string name) : base(name)
    {
    }
}


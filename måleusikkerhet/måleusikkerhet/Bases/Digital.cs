using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Digital : DevBase
{
    
    public List<DigitalAttributes> Ranges { get; set; } = new();
    public double? Resolution { get; set; } = double.NegativeInfinity;

    public override double GetUncertanty(double value,double frequency = -1)
    {
        var current = Ranges.OrderBy(x => x.Range).First(x => x.Range > value && (x.Frequency == null || x.Frequency > frequency));
        return value * current.RangeError + current.Digits * Resolution ?? double.NegativeInfinity;
    }

    public Digital(string name) : base(name)
    {
    }
}


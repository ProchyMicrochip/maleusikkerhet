using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Digital : DevBase
{
    
    public List<DigitalAttributes> Ranges { get; set; } = new();
    public double? Resolution { get; set; } = double.NegativeInfinity;

    public override double GetUncertanty(double value)
    {
        var current = Ranges.OrderBy(x => x.Range).First(x => x.Range > value);
        return value * current.RangeError + current.Digits * Resolution ?? double.NegativeInfinity;
    }

    public Digital(string name) : base(name)
    {
    }
}


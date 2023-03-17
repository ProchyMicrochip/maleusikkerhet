using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;

namespace måleusikkerhet.Bases;

public class Analog : DevBase
{
    public List<AnalogAttributes> Ranges { get; set; } = new();
    public override double GetUncertanty(double value, double frequency = -1)
    {
        var current = Ranges.OrderBy(x => x.Range).First(x => x.Range > value);
        return current.Range * current.Precision + 0.5 * current.Range / RelativeRange;
    }

    public Analog(string name) : base(name)
    {
    }
    
    public int RelativeRange { get; set; }
}
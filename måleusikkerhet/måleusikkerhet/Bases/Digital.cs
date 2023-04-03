using System.Collections.Generic;
using System.Linq;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Bases;

public class Digital : DevBase
{
    
    public List<DigitalAttributes> Ranges { get; set; } = new();

    public override Precision GetUncertanty(Precision value,Precision frequency, MeasurementType type)
    {
        var current = Ranges.Where(x => x.MeasurementType == type).OrderBy(x => x.Range).First(x => x.Range > value && (x.Frequency == null || x.Frequency > frequency));
        return (value * current.MeasureError * new Precision(1,-2) + current.Digits * current.Resolution)/new Precision(1732050,-6);
    }

    public Digital(string name) : base(name)
    {
    }
}


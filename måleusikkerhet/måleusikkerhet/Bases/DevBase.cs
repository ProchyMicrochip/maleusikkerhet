using System.ComponentModel.DataAnnotations;
using måleusikkerhet.Database;
using måleusikkerhet.Infrastructure;
using måleusikkerhet.Interface;

namespace måleusikkerhet.Bases;

public abstract class DevBase : IUncertainty
{
    protected DevBase(string name)
    {
        Name = name;
    }
    [Key]
    public string Name { get; set; }
    public abstract Precision GetUncertanty(Precision value, Precision frequency, MeasurementType type);
    
    public ImageDb? Image { get; set; }
    
}
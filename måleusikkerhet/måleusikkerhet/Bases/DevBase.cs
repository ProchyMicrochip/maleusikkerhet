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
    public abstract double GetUncertanty(double value);
    
    public ImageDb? Image { get; set; }
    
    public MeasurementType MeasumentType { get; set; }
}
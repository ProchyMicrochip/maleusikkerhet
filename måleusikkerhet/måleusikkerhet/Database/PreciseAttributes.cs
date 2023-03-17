using System.ComponentModel.DataAnnotations;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Database;

public class PreciseAttributes
{
    [Key]
    public int Id { get; set; }
    
    public MeasurementType MeasurementType { get; set; }
    public double Range { get; set; }
    public double MeasureError { get; set; }
    public double RangeError { get; set; }
    
    public double? Frequency { get; set; }
}
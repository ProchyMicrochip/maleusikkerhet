using System.ComponentModel.DataAnnotations;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Database;

public class PreciseAttributes
{
    [Key]
    public int Id { get; set; }
    
    public MeasurementType MeasurementType { get; set; }
    public Precision Range { get; set; }
    public Precision MeasureError { get; set; }
    public Precision RangeError { get; set; }
    
    public Precision? Frequency { get; set; }
}
using System.ComponentModel.DataAnnotations;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Database;

public class DigitalAttributes
{
    [Key]
    public int Id { get; set; }
    
    public MeasurementType MeasurementType { get; set; }
    public Precision Range { get; set; }
    public Precision Digits { get; set; }
    public Precision RangeError { get; set; }
    
    public Precision? Frequency { get; set; }
}
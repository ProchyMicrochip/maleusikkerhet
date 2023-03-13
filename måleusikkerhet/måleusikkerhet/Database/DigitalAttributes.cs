using System.ComponentModel.DataAnnotations;

namespace måleusikkerhet.Infrastructure;

public class DigitalAttributes
{
    [Key]
    public int Id { get; set; }
    public double Range { get; set; }
    public double Digits { get; set; }
    public double RangeError { get; set; }
}
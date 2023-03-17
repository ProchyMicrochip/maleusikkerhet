using System;
using System.ComponentModel.DataAnnotations;

namespace måleusikkerhet.Database;

public class AnalogAttributes
{
    [Key]
    public int Id { get; set; }
    public double Precision { get; set; }
    public double Range { get; set; }
}
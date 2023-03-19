using System;
using System.ComponentModel.DataAnnotations;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Database;

public class AnalogAttributes
{
    [Key]
    public int Id { get; set; }
    public Precision Precision { get; set; }
    public Precision Range { get; set; }
}
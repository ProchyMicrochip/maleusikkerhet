﻿using System.ComponentModel.DataAnnotations;
using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Database;

public class DigitalAttributes
{
    [Key]
    public int Id { get; set; }
    
    public MeasurementType MeasurementType { get; set; }
    public double Range { get; set; }
    public double Digits { get; set; }
    public double RangeError { get; set; }
    
    public double? Frequency { get; set; }
}
using måleusikkerhet.Database;
using måleusikkerhet.Models;

namespace måleusikkerhet.Infrastructure;

public class DigitalAtributesToModel
{
    public static DigitalAttributesModel ToModel(DigitalAttributes attributes) =>
        new(attributes.MeasurementType,
            attributes.Range,
            attributes.Frequency,
            attributes.MeasureError,
            attributes.Digits,
            attributes.Resolution);

    public static DigitalAttributes FromModel(DigitalAttributesModel model) =>
        new()
        {
            Digits = model.Digits ?? Precision.Zero,
            MeasureError = model.MeasureError ?? Precision.Zero,
            Range = model.Range ?? Precision.Zero,
            Resolution = model.Resolution ?? Precision.Zero,
            Frequency = model.Frequency,
            MeasurementType = model.Type
        };
}
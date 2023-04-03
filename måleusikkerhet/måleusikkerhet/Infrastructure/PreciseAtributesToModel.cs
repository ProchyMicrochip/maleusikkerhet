using System.Collections.Generic;
using måleusikkerhet.Database;
using måleusikkerhet.Models;

namespace måleusikkerhet.Infrastructure;

public static class PreciseAtributesToModel
{
    public static PreciseAttributesModel ToModel(PreciseAttributes attributes) =>
        new(attributes.MeasurementType,
            attributes.RangeError,
            attributes.MeasureError,
            attributes.Range,
            attributes.Frequency);

    public static PreciseAttributes FromModel(PreciseAttributesModel model) =>
        new()
        {
            RangeError = model.RangeError ?? Precision.Zero,
            MeasureError = model.MeasureError ?? Precision.Zero,
            Range = model.Range ?? Precision.Zero,
            Frequency = model.Frequency,
            MeasurementType = model.Type
        };
}
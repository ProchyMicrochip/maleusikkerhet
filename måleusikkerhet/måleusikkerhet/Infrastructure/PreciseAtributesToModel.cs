using System.Collections.Generic;
using måleusikkerhet.Database;

namespace måleusikkerhet.Infrastructure;

public static class PreciseAtributesToModel
{
    public static PreciseAttributesModel ToModel(PreciseAttributes attributes) =>
        new(attributes.MeasurementType, attributes.RangeError, attributes.MeasureError, attributes.Range,
            attributes.Frequency);

    public static PreciseAttributes FromModel(PreciseAttributesModel model) =>
        new()
        {
            RangeError = model.RangeError ?? new Precision(0, 0),
            MeasureError = model.MeasureError ?? new Precision(0, 0),
            Range = model.Range ?? new Precision(0, 0),
            Frequency = model.Frequency,
            MeasurementType = model.Type
        };
}
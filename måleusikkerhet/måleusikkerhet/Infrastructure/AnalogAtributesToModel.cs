using måleusikkerhet.Database;
using måleusikkerhet.Models;

namespace måleusikkerhet.Infrastructure;

public class AnalogAtributesToModel
{
    public static AnalogAttributesModel ToModel(AnalogAttributes attributes) =>
        new(attributes.MeasurementType, attributes.Precision, attributes.Range);

    public static AnalogAttributes FromModel(AnalogAttributesModel attributesModel) => new AnalogAttributes()
    {
        MeasurementType = attributesModel.Type,
        Precision = attributesModel.Precision ?? Precision.Zero,
        Range = attributesModel.Range ?? Precision.Zero
    };
}
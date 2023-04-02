using måleusikkerhet.Infrastructure;

namespace måleusikkerhet.Interface;

public interface IUncertainty
{
    public Precision GetUncertanty(Precision value, Precision frequency, MeasurementType type);
}
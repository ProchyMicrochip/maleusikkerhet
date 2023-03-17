namespace måleusikkerhet.Interface;

public interface IUncertainty
{
    public double GetUncertanty(double value, double frequency = -1);
}
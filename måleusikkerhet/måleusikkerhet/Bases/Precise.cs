using måleusikkerhet.Interface;

namespace måleusikkerhet.Bases;

public class Precise : DevBase
{
    public override double GetUncertanty(double value)
    {
        throw new System.NotImplementedException();
    }

    public Precise(string name) : base(name)
    {
    }
}
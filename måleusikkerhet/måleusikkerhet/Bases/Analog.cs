using måleusikkerhet.Interface;

namespace måleusikkerhet.Bases;

public class Analog : DevBase
{
    public override double GetUncertanty(double value)
    {
        throw new System.NotImplementedException();
    }

    public Analog(string name) : base(name)
    {
    }
}
namespace måleusikkerhet.Infrastructure;

public static class UncertantyRounder
{
    public static Precision RoundUncertanty(this Precision precision)
    {
        var a = precision.Mantis.ToString();
        string mantis;
        int add;
        if (a.Length <= 1) return precision;
        if (a[0] is '1' or '2')
        {
            mantis = a[..2];
            add = a.Length - 2;
        }
        else
        {
            mantis = a[..1];
            add = a.Length - 1;
        }
        return new Precision(long.Parse(mantis)+1, precision.Exponent + add);
    }

    public static string ToPercentage(this Precision precision)
    {
        var a = (precision*new Precision(1,2)).RoundUncertanty();
        return a + "%";
    }
    public static string ToFull(Precision value, Precision uncertanty)
    {
        return value + " ± " + uncertanty.RoundUncertanty();
    }
}
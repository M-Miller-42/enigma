public class Involution : Permutation
{
    private static Involution? identity;
    public static new Involution Identity {
        get {
            identity ??= new Involution(Permutation.Identity);
            return identity;
        }
    }

    public Involution(Permutation permutation) : base(permutation.Table)
    {
        if (!IsInvolution())
            throw new FormatException();
    }

    private bool IsInvolution()
    {
        return Enumerable.Range(0, Enigma.N).All(i => ApplyTo(ApplyTo(i)) == i);
    }

    public static new Involution Parse(string p)
    {
        if (p == "random")
            return Random();
        return new Involution(Permutation.Parse(p));
    }

    public static new Involution Random()
    {
        int pairs = Enigma.N / 2;
        int[] involution = new int[Enigma.N];
        Permutation p = Permutation.Random();

        for (int i = 0; i <= 2 * (pairs - 1); i += 2)
        {
            involution[p.ApplyTo(i)] = p.ApplyTo(i + 1);
            involution[p.ApplyTo(i + 1)] = p.ApplyTo(i);
        }

        if (Enigma.N % 2 == 1)
            involution[p.ApplyTo(Enigma.N - 1)] = p.ApplyTo(Enigma.N - 1);

        return new Involution(new Permutation(involution));
    }
}

public class Involution : Permutation
{
    public override int[] Perm
    {
        get
        {
            return base.Perm;
        }
        set
        {
            int[] old = base.Perm;
            base.Perm = value;
            if (!IsInvolution())
            {
                base.Perm = old;
                throw new FormatException();
            }
        }
    }
    private static Involution? identity;
    public static new Involution Identity {
        get {
            identity ??= new Involution(Permutation.Identity);
            return identity;
        }
    }


    public Involution(Permutation perm) : this(perm.Perm) {}

    public Involution(int[] perm) : base(perm)
    {
        if (!IsInvolution())
            throw new FormatException();
    }

    private bool IsInvolution()
    {
        for (int i = 0; i <= Enigma.n - 1; i++)
        {
            if (ApplyTo(ApplyTo(i)) != i)
                return false;
        }
        return true;
    }

    public static new Involution Parse(string p)
    {
        if (p == "random")
            return Random();
        return new Involution(Permutation.Parse(p));
    }

    public static new Involution Random()
    {
        int pairs = Enigma.n / 2;
        int[] involution = new int[Enigma.n];
        Permutation p = Permutation.Random();

        for (int i = 0; i <= 2 * (pairs - 1); i += 2)
        {
            involution[p.ApplyTo(i)] = p.ApplyTo(i + 1);
            involution[p.ApplyTo(i + 1)] = p.ApplyTo(i);
        }

        if (Enigma.n % 2 == 1)
            involution[p.ApplyTo(Enigma.n - 1)] = p.ApplyTo(Enigma.n - 1);

        return new Involution(involution);
    }
}

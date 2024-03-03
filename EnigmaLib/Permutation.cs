using System.Collections.Immutable;

public partial class Permutation
{
    private static Random RandomGen = new Random();
    private static Permutation? _identity;
    public static Permutation Identity
    {
        get
        {
            _identity ??= new Permutation(Enumerable.Range(0, Enigma.N));
            return _identity;
        }
    }

    /*
        The i-th array value is the image of i as defined by the permutation.
    */
    public ImmutableArray<int> Table { get; }
    private int[] _inv;


    public int ApplyTo(int i)
    {
        return Table[i];
    }

    public int ApplyInverseTo(int i)
    {
        return _inv[i];
    }


    public Permutation(IEnumerable<int> permutation)
    {
        if (!IsPermutation(permutation))
            throw new FormatException();
        this.Table = permutation.ToImmutableArray();
        CreateInverse();
    }


    private static bool IsPermutation(IEnumerable<int> permutation)
    {
        if (permutation == null || permutation.Count() != Enigma.N)
            return false;

        return permutation.ToHashSet().SetEquals(Enumerable.Range(0, Enigma.N));
    }

    public void CreateInverse()
    {
        _inv = new int[Enigma.N];
        for (int i = 0; i < Enigma.N; i++)
            _inv[Table[i]] = i;
    }

    public static Permutation Parse(string p)
    {
        if (p == "identity")
            return Identity;
        if (p == "random")
            return Random();
        if (Enigma.Alphabet.Contains(p))
            return new Permutation(p.Select(Enigma.Alphabet.ToInt));
        var permutation = p.Split(',').Select(int.Parse);
        return new Permutation(permutation);
    }

    public static Permutation Random()
    {
        int[] permutation = Enumerable.Range(0, Enigma.N).ToArray();
        RandomGen.Shuffle(permutation);
        return new Permutation(permutation);
    }


    public override string ToString()
    {
        var permutation = Enumerable.Range(0, Enigma.N)
            .Select(ApplyTo)
            .Select(Enigma.Alphabet.ToChar);
        return string.Concat(permutation);
    }
}

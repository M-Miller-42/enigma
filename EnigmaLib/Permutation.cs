using System.Collections.Immutable;
using System.Text.RegularExpressions;

public partial class Permutation
{
    [GeneratedRegex("[A-Z]+")]
    public static partial Regex Alphabet();

    private static Random RandomGen = new Random();
    private static Permutation? _identity;
    public static Permutation Identity
    {
        get
        {
            if (_identity == null)
            {
                _identity = new Permutation(Enumerable.Range(0, Enigma.n));
            }
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
        bool[] l = new bool[Enigma.n ];
        if (permutation == null || permutation.Count() != Enigma.n)
            return false;

        foreach (int c in permutation)
        {
            if (c < 0 || c >= Enigma.n || l[c] == true)
                return false;
            else
                l[c] = true;
        }

        return true;
    }

    public void CreateInverse()
    {
        _inv = new int[Enigma.n];
        for (int i = 0; i < Enigma.n; i++)
            _inv[Table[i]] = i;
    }

    public static Permutation Parse(string p)
    {
        if (p == "identity")
            return Identity;
        if (p == "random")
            return Random();
        if (Alphabet().IsMatch(p))
            return new Permutation(p.Select(parseFromAlphabet));
        var permutation = p.Split(',').Select(int.Parse);
        return new Permutation(permutation);
    }

    public static int parseFromAlphabet(char c){
        return Convert.ToInt32(c) - 65;
    }


    public static Permutation Random()
    {
        int[] permutation = Enumerable.Range(0, Enigma.n).ToArray();
        RandomGen.Shuffle(permutation);
        return new Permutation(permutation);
    }


    public override string ToString()
    {
        var permutation = Enumerable.Range(0, Enigma.n)
            .Select(ApplyTo)
            .Select(c => Convert.ToChar(c + 65));
        return string.Join("", permutation);
    }
}

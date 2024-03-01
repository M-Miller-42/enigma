using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

public partial class Permutation
{
    [GeneratedRegex("[A-Z]+")]
    public static partial Regex Alphabet();

    private static Random rand = new Random();
    private static Permutation _identity;
    public static Permutation Identity
    {
        get
        {
            if (_identity == null)
            {
                int[] perm = new int[Enigma.n ];
                for (int i = 0; i <= Enigma.n - 1; i++)
                    perm[i] = i;
                _identity = new Permutation(perm);
            }
            return _identity;
        }
    }

    private int[] _perm;
    public virtual int[] Perm
    {
        get
        {
            return _perm;
        }
        set
        {
            if (!IsPermutation(value))
                throw new FormatException();
            _perm = value;
        }
    }

    public virtual int Forward(int i)
    {
        return _perm[i];
    }

    private int[] _inv;
    public virtual int Backward(int i)
    {
        return _inv[i];
    }


    public Permutation(IEnumerable<int> perm)
    {
        if (!IsPermutation(perm))
            throw new FormatException();
        this._perm = perm.ToArray();
        CreateInverse();
    }


    private bool IsPermutation(IEnumerable<int> permutation)
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
        int[] p = new int[Enigma.n ];
        for (int i = 0; i <= Enigma.n - 1; i++)
            p[_perm[i]] = i;
        _inv = p;
    }

    public static Permutation Parse(string p)
    {
        if (p == "identity")
            return Identity;
        if (p == "random")
            return Random();
        if (Alphabet().IsMatch(p))
            return new Permutation(p.Select(parseFromAlphabet));
        var perm = p.Split(',').Select(int.Parse);
        return new Permutation(perm);
    }

    public static int parseFromAlphabet(char c){
        return Convert.ToInt32(c) - 65;
    }


    public static Permutation Random()
    {
        List<int> l = new List<int>();
        int r;
        int[] perm = new int[Enigma.n];
        for (int i = 0; i <= Enigma.n - 1; i++)
            l.Add(i);
        for (int i = 0; i <= Enigma.n - 1; i++)
        {
            r = rand.Next(0, l.Count - 1);
            perm[i] = l[r];
            l.RemoveAt(r);
        }
        return new Permutation(perm);
    }


    public override string ToString()
    {
        // string result = string.Empty;
        // for (int i = 0; i <= _perm.Count() - 2; i++)
        //     result += _perm[i] + ",";
        // result += _perm[_perm.Count() - 1];
        // return result;

        var perm = Enumerable.Range(0, Enigma.n).Select(i => _perm[i]);
        var result = perm.Select(c => Convert.ToChar(c + 65));
        return string.Join("", result);
    }
}

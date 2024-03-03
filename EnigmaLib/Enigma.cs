namespace EnigmaLib;
public partial class Enigma
{
    public static int N => Alphabet.Count;
    private static IAlphabet alphabet = new LatinAlphabet();

    public static IAlphabet Alphabet
    {
        get => alphabet;
        set
        {
            if (value == null)
                throw new NullReferenceException();
            alphabet = value;
        }
    }

    private PatchBoard _patchBoard;
    public PatchBoard PatchBoard
    {
        get
        {
            return _patchBoard;
        }
        set
        {
            if (value == null)
                throw new NullReferenceException();
            _patchBoard = value;
        }
    }

    private Reflector _reflector;
    public Reflector Reflector
    {
        get
        {
            return _reflector;
        }
        set
        {
            if (value == null)
                throw new NullReferenceException();
            _reflector = value;
        }
    }


    private Rotor[] _rotors = [];

    public Rotor GetRotor(int index)
    {
        if (index >= _rotors.Length)
            throw new ArgumentOutOfRangeException();
        return _rotors[index];
    }

    public void SetRotor(int index, Rotor value)
    {
        if (value == null)
            throw new NullReferenceException();
        if (index >= _rotors.Length)
            throw new ArgumentOutOfRangeException();
        _rotors[index] = value;
    }



    public Enigma(RotorParam[] rotorParams, Involution reflectorPerm, Permutation patchBoardPerm)
    {
        if (rotorParams == null)
            throw new NullReferenceException();
        if (!IsValidRotorParamList(rotorParams))
            throw new FormatException();
        Reflector = new Reflector(reflectorPerm);
        PatchBoard = new PatchBoard(patchBoardPerm);
        CreateRotors(rotorParams);
    }

    private static bool IsValidRotorParamList(RotorParam[] rotorParams)
    {
        return rotorParams.All(param => param != null);
    }

    private void CreateRotors(RotorParam[] rotorParams)
    {
        Rotor? next = null;
        _rotors = new Rotor[rotorParams.Length];
        for (int i = rotorParams.Length - 1; i >= 0; i--)
        {
            _rotors[i] = new Rotor(rotorParams[i], next);
            next = _rotors[i];
        }
    }

    public string EncodeString(string input)
    {
        var ints = input.Select(Alphabet.ToInt);
        var output = Encode(ints);
        var chars = output.Select(Alphabet.ToChar);
        return string.Concat(chars);
    }

    public IEnumerable<int> Encode(IEnumerable<int> input)
    {
        return Encode(input.ToArray());
    }

    public int[] Encode(int[] input)
    {
        var res = new int[input.Count()];
        int index = 0;
        foreach (int c in input)
        {
            int current = PatchBoard.ApplyTo(c);


            for (int ii = 0; ii < _rotors.Length; ii++)
                current = _rotors[ii].Forward(current);

            current = _reflector.ApplyTo(current);

            for (int ii = _rotors.Length - 1; ii >= 0; ii--)
                current = _rotors[ii].Backward(current);

            current = PatchBoard.ApplyInverseTo(current);

            res[index] = current;
            _rotors[0].Tick();
            index += 1;
        }
        return res;
    }

    public override string ToString()
    {
        string result = string.Empty;
        result += $"{_rotors.Length}\n";
        result += $"{PatchBoard}\n";
        for (int i = 0; i < _rotors.Length; i++)
            result += $"R{i}:\t{_rotors[i]}\n";
        result += $"{_reflector}\n";
        return result;
    }
}

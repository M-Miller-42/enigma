public class Enigma
{
    public const int n = 26;

    private PatchBoard _patchBoard;
    public PatchBoard PatchBoard
    {
        get
        {
            return _patchBoard;
        }
        set
        {
            if (PatchBoard == null)
                throw new NullReferenceException();
            _patchBoard = PatchBoard;
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


    private Rotor[] _rotors = Array.Empty<Rotor>();
    public Rotor getRotor(int index)
    {
        if (index >= _rotors.Length)
            throw new ArgumentOutOfRangeException();
        return _rotors[index];
    }

    public void getRotor(int index, Rotor value)
    {
        if (value == null)
            throw new NullReferenceException();
        if (index >= _rotors.Length)
            throw new ArgumentOutOfRangeException();
        _rotors[index] = value;
    }



    public Enigma(RotorParam[] rotorParams, Involution reflectorPerm, Permutation patchBoardPerm)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException();
        if (rotorParams == null)
            throw new NullReferenceException();
        if (!isValidRotorParamList(rotorParams))
            throw new FormatException();
        _reflector = new Reflector(reflectorPerm);
        _patchBoard = new PatchBoard(patchBoardPerm);
        CreateRotors(rotorParams);
    }

    private bool isValidRotorParamList(RotorParam[] rotorParams)
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


    public static bool isChar(int a)
    {
        return a >= 0 && a < n;
    }
    public string EncodeString(string input)
    {
        var ints = input.Select(c => Convert.ToInt32(c) - 65);
        var output = Encode(ints);
        var chars = output.Select(c => Convert.ToChar(c + 65));
        return string.Concat(chars);
    }

    public IEnumerable<int> Encode(IEnumerable<int> input)
    {
        return Encode(input.ToArray());
    }

    public int[] Encode(int[] input)
    {
        var res = new int[input.Count()];
        int i = 0;
        int j = 0;
        foreach (int c in input)
        {
            j = _patchBoard.ApplyTo(c);


            for (int ii = 0; ii < _rotors.Length; ii++)
                j = _rotors[ii].Forward(j);

            j = _reflector.ApplyTo(j);

            for (int ii = _rotors.Length - 1; ii >= 0; ii--)
                j = _rotors[ii].Backward(j);

            j = _patchBoard.ApplyInverseTo(j);

            res[i] = j;
            _rotors[0].Tick();
            i += 1;
        }
        return res;
    }

    public override string ToString()
    {
        string result = string.Empty;
        result += $"{_rotors.Length}\n";
        result += $"{_patchBoard}\n";
        for (int i = 0; i < _rotors.Length; i++)
            result += $"R{i}:\t{_rotors[i]}\n";
        result += $"{_reflector}\n";
        return result;
    }
}

public class Rotor
{
    public Permutation Permutation { get; }
    private int _Index;
    public int Index
    {
        get
        {
            return _Index;
        }
        set
        {
            _Index = ((value % Enigma.n) + Enigma.n) % Enigma.n;
        }
    }

    private int _TickPos;
    public int TickPos
    {
        get
        {
            return _TickPos;
        }
        set
        {
            _TickPos = ((value % Enigma.n) + Enigma.n) % Enigma.n;
        }
    }

    public Rotor? NextRotor { get; }



    public int Forward(int i)
    {
        return (Permutation.ApplyTo((i + _Index + Enigma.n) % Enigma.n) - _Index + Enigma.n) % Enigma.n;
        // return ((base.Forward((i + _Index) % Enigma.n) - _Index) % Enigma.n + Enigma.n) % Enigma.n;
    }

    public int Backward(int i)
    {
        return (Permutation.ApplyInverseTo((i + _Index + Enigma.n) % Enigma.n) - _Index + Enigma.n) % Enigma.n;
        // return ((base.Backward((i + _Index) % Enigma.n) - _Index) % Enigma.n + Enigma.n) % Enigma.n;
    }

    public Rotor(RotorParam rp, Rotor? nextRotor)
    {
        this.Permutation = rp.Permutation;
        this.Index = rp.Index;
        this.TickPos = rp.TickPos;
        this.NextRotor = nextRotor;
    }


    public virtual void Tick()
    {
        if (_Index == _TickPos)
            NextRotor?.Tick();
        _Index = (_Index + 1) % Enigma.n;
    }

    public override string ToString()
    {
        return string.Join(" ", Permutation.ToString(), TickPos, Index);
    }
}

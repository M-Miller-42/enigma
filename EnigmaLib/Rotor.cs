using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

public class Rotor : Permutation
{
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

    private Rotor? _nextRotor;
    public Rotor? NextRotor
    {
        get
        {
            return _nextRotor;
        }
    }



    public override int Forward(int i)
    {
        return (base.Forward((i + _Index + Enigma.n) % Enigma.n) - _Index + Enigma.n) % Enigma.n;
        // return ((base.Forward((i + _Index) % Enigma.n) - _Index) % Enigma.n + Enigma.n) % Enigma.n;
    }

    public override int Backward(int i)
    {
        return (base.Backward((i + _Index + Enigma.n) % Enigma.n) - _Index + Enigma.n) % Enigma.n;
        // return ((base.Backward((i + _Index) % Enigma.n) - _Index) % Enigma.n + Enigma.n) % Enigma.n;
    }

    public Rotor(RotorParam rp, Rotor? nextRotor) : base(rp.Perm.Perm)
    {
        _Index = rp.Index;
        _TickPos = rp.TickPos;
        this._nextRotor = nextRotor;
    }


    public virtual void Tick()
    {
        if (_Index == _TickPos)
            _nextRotor?.Tick();
        _Index = (_Index + 1) % Enigma.n;
    }

    public override string ToString()
    {
        return string.Join(" ", base.ToString(), TickPos, Index);
    }
}
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

public class NullRotor : Rotor
{
    public override int Forward(int i)
    {
        throw new AccessViolationException();
    }

    public override int Backward(int i)
    {
        throw new AccessViolationException();
    }

    public override int[] Perm
    {
        get
        {
            throw new AccessViolationException();
        }
        set
        {
            throw new AccessViolationException();
        }
    }

    public NullRotor() : base(new RotorParam(Permutation.Identity, 0, 0), null/* TODO Change to default(_) if this is not a reference type */)
    {
    }

    public override void Tick()
    {
    }

    public override string ToString()
    {
        return "NULLROTOR!!!";
    }
}

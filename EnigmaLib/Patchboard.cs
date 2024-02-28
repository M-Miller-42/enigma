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

public class PatchBoard : Permutation
{
    public PatchBoard(Permutation perm) : base(perm.Perm)
    {
    }

    public override string ToString()
    {
        return "PB: " + Constants.vbTab + base.ToString();
    }
}

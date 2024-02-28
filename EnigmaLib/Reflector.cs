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

public class Reflector : Involution
{
    public Reflector(Involution perm) : base(perm)
    {
    }

    public override string ToString()
    {
        return "Ref: " + Constants.vbTab + base.ToString();
    }
}

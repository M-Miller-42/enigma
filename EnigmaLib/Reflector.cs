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

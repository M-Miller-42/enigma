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

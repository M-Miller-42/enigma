public class PatchBoard(Permutation permutation) : Permutation(permutation.Table)
{
    public override string ToString()
    {
        return $"PB:\t{base.ToString()}";
    }
}

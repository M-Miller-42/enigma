public class Reflector(Involution involution) : Involution(involution)
{
    public override string ToString()
    {
        return $"Ref:\t{base.ToString()}";
    }
}

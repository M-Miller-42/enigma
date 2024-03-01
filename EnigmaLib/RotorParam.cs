public class RotorParam
{
    public int TickPos {get; }

    public int Index { get; }

    public Permutation Perm { get; }


    public RotorParam(Permutation perm, int tickPos, int index)
    {
        if (Enigma.n < 0)
            throw new ArgumentOutOfRangeException();
        if (tickPos < 0 || tickPos >= Enigma.n)
            throw new ArgumentOutOfRangeException();
        if (index < 0 || index >= Enigma.n)
            throw new ArgumentOutOfRangeException();
        this.Perm = perm;
        this.TickPos = tickPos;
        this.Index = index;
    }

    public static RotorParam Parse(string permutation, string tickPos, string index){
        int parsedTickPos = Permutation.Alphabet().IsMatch(tickPos) && tickPos.Length == 1 ? 
            Permutation.parseFromAlphabet(tickPos[0]) : 
            int.Parse(tickPos);
        int parsedIndex = Permutation.Alphabet().IsMatch(index) && index.Length == 1 ? 
            Permutation.parseFromAlphabet(index[0]) : 
            int.Parse(index);
        return new RotorParam(
            Permutation.Parse(permutation),
            parsedTickPos,
            parsedIndex
        );
    }
}

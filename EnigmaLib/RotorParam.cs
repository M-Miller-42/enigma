namespace EnigmaLib;
public class RotorParam
{
    public int TickPos {get; }

    public int Index { get; }

    public Permutation Permutation { get; }


    public RotorParam(Permutation permutation, int tickPos, int index)
    {
        if (Enigma.N < 0)
            throw new ArgumentOutOfRangeException();
        if (tickPos < 0 || tickPos >= Enigma.N)
            throw new ArgumentOutOfRangeException();
        if (index < 0 || index >= Enigma.N)
            throw new ArgumentOutOfRangeException();
        this.Permutation = permutation;
        this.TickPos = tickPos;
        this.Index = index;
    }

    public static RotorParam Parse(string permutation, string tickPos, string index){
        int parsedTickPos = Enigma.Alphabet.Contains(tickPos) && tickPos.Length == 1 ?
            Enigma.Alphabet.ToInt(tickPos[0]) :
            int.Parse(tickPos);
        int parsedIndex = Enigma.Alphabet.Contains(index) && index.Length == 1 ?
            Enigma.Alphabet.ToInt(index[0]) :
            int.Parse(index);
        return new RotorParam(
            Permutation.Parse(permutation),
            parsedTickPos,
            parsedIndex
        );
    }
}

public interface IAlphabet
{
    public int Count { get; }

    public abstract bool Contains(string word);

    public abstract char ToChar(int i);
    public abstract int ToInt(char c);

}
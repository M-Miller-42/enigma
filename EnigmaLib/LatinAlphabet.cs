using System.Text.RegularExpressions;

public partial class LatinAlphabet : IAlphabet
{
    int IAlphabet.Count => 26;

    public bool Contains(string word)
    {
        return Test().IsMatch(word);
    }

    [GeneratedRegex("[A-Z]+")]

    public partial Regex Test();

    public char ToChar(int i)
    {
        return Convert.ToChar(i + 65);
    }

    public int ToInt(char c)
    {
        return Convert.ToInt32(c) - 65;
    }
}
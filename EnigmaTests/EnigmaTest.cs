namespace EnigmaTests;

[TestClass]
public class EnigmaTest
{
    [TestInitialize]
    public void Reset()
    {
        EnigmaConsole.Reset();
    }

    [TestMethod]
    public void TestIdentity()
    {
        PatchBoard patchBoard = new PatchBoard(Permutation.Identity);
        Reflector reflector = new Reflector(Involution.Identity);
        RotorParam[] rotorParams = { new(Permutation.Identity, 0, 0) };
        Enigma enigma = new Enigma(rotorParams, reflector, patchBoard);
        Assert.AreEqual("ABCBA", enigma.EncodeString("ABCBA"));
    }

    [TestMethod]
    public void TestTick()
    {
        PatchBoard patchBoard = new PatchBoard(Permutation.Identity);
        Reflector reflector = new Reflector(Involution.Identity);
        RotorParam[] rotorParams = { new(Permutation.Identity, 0, 0), new(Permutation.Identity, 0, 0) };
        Enigma enigma = new Enigma(rotorParams, reflector, patchBoard);
        Rotor r0 = enigma.getRotor(0);
        Rotor r1 = enigma.getRotor(1);
        r0.Tick();
        Assert.AreEqual(1, r0.Index);
        Assert.AreEqual(1, r1.Index);
    }


    [TestMethod]
    public void TestExample1()
    {
        EnigmaConsole.ParseLine("!create 3 LPGSZMHAEOQKVXRFYBUTNICJDW Z V SLVGBTFXJQOHEWIRZYAMKPCNDU Z Q CJGDPSHKTURAWZXFMYNQOBVLIE Z I IMETCGFRAYSQBZXWLHKDVUPOJN JWULCMNOHPQZYXIRADKEGVBTSF");
        Assert.AreEqual("PFXWAKVSTNRRKX", EnigmaConsole.Enigma.EncodeString("HALLOPRAKTOMAT"));
    }

    [TestMethod]
    public void TestExample2()
    {
        EnigmaConsole.ParseLine("!create 3 LPGSZMHAEOQKVXRFYBUTNICJDW Z V SLVGBTFXJQOHEWIRZYAMKPCNDU Z Q CJGDPSHKTURAWZXFMYNQOBVLIE Z I IMETCGFRAYSQBZXWLHKDVUPOJN JWULCMNOHPQZYXIRADKEGVBTSF");
        Assert.AreEqual("HALLOPRAKTOMAT", EnigmaConsole.Enigma.EncodeString("PFXWAKVSTNRRKX"));
    }

    [TestMethod]
    public void TestExample3()
    {
        EnigmaConsole.ParseLine("!create 3 LPGSZMHAEOQKVXRFYBUTNICJDW Z Q SLVGBTFXJQOHEWIRZYAMKPCNDU Z V CJGDPSHKTURAWZXFMYNQOBVLIE Z I IMETCGFRAYSQBZXWLHKDVUPOJN JWULCMNOHPQZYXIRADKEGVBTSF");
        Assert.AreEqual("OHHPTLICSLUIVU", EnigmaConsole.Enigma.EncodeString("PFXWAKVSTNRRKX"));
    }

    [TestMethod]
    public void TestParsing()
    {
        EnigmaConsole.ParseLine("!create 0 random");
        Assert.IsNotNull(EnigmaConsole.Enigma);
        EnigmaConsole.Reset();

        EnigmaConsole.ParseLine("!create 1 random 0 0 random identity");
        Assert.IsNotNull(EnigmaConsole.Enigma);
        EnigmaConsole.Reset();

        EnigmaConsole.ParseLine("!create 0 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25 random");
        Assert.IsNotNull(EnigmaConsole.Enigma);
        EnigmaConsole.Reset();

    }
}
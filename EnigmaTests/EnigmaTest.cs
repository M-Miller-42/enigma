namespace EnigmaTests;

[TestClass]
public class EnigmaTest
{
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
        EnigmaConsole.ParseLine("create 3 LPGSZMHAEOQKVXRFYBUTNICJDW Z V SLVGBTFXJQOHEWIRZYAMKPCNDU Z Q CJGDPSHKTURAWZXFMYNQOBVLIE Z I IMETCGFRAYSQBZXWLHKDVUPOJN JWULCMNOHPQZYXIRADKEGVBTSF");
        Assert.AreEqual("PFXWAKVSTNRRKX", EnigmaConsole.enigma.EncodeString("HALLOPRAKTOMAT"));
    }

    [TestMethod]
    public void TestExample2()
    {
        EnigmaConsole.ParseLine("create 3 LPGSZMHAEOQKVXRFYBUTNICJDW Z V SLVGBTFXJQOHEWIRZYAMKPCNDU Z Q CJGDPSHKTURAWZXFMYNQOBVLIE Z I IMETCGFRAYSQBZXWLHKDVUPOJN JWULCMNOHPQZYXIRADKEGVBTSF");
        Assert.AreEqual("HALLOPRAKTOMAT", EnigmaConsole.enigma.EncodeString("PFXWAKVSTNRRKX"));
    }

    [TestMethod]
    public void TestExample3()
    {
        EnigmaConsole.ParseLine("create 3 LPGSZMHAEOQKVXRFYBUTNICJDW Z Q SLVGBTFXJQOHEWIRZYAMKPCNDU Z V CJGDPSHKTURAWZXFMYNQOBVLIE Z I IMETCGFRAYSQBZXWLHKDVUPOJN JWULCMNOHPQZYXIRADKEGVBTSF");
        Assert.AreEqual("OHHPTLICSLUIVU", EnigmaConsole.enigma.EncodeString("PFXWAKVSTNRRKX"));
    }
}
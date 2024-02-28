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
}
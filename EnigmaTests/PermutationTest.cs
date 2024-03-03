using EnigmaLib;

namespace EnigmaTests;
[TestClass]
public class PermutationTest
{

    [TestMethod]
    public void TestIdentity()
    {
        var expected = Enumerable.Range(0, Enigma.N);
        var actual = Enumerable.Range(0, Enigma.N).Select(Permutation.Identity.ApplyTo);
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestInverse()
    {
        var permutation = Permutation.Random();
        var expected = Enumerable.Range(0, Enigma.N);
        var actual = expected.Select(i => permutation.ApplyInverseTo(permutation.ApplyTo(i)));
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }

    [TestMethod]
    public void TestInvolution()
    {
        var involution = Involution.Random();
        var expected = Enumerable.Range(0, Enigma.N);
        var actual = expected.Select(i => involution.ApplyTo(involution.ApplyTo(i)));
        CollectionAssert.AreEqual(expected.ToArray(), actual.ToArray());
    }
}
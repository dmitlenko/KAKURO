using Kakuro.Engine;
using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;


namespace Kakuro.Test;

[TestClass]
public class GeneratorTest
{
    [TestMethod]
    public void Test1()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(4, 4, 1);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 4);
        Assert.AreEqual(b.Height, 4);
        Assert.AreEqual(b.Difficulty, 1);
    }

    [TestMethod]
    public void Test2()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(6, 6, 2);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 6);
        Assert.AreEqual(b.Height, 6);
        Assert.AreEqual(b.Difficulty, 2);
    }

    [TestMethod]
    public void Test3()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(6, 5, 2);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 6);
        Assert.AreEqual(b.Height, 5);
        Assert.AreEqual(b.Difficulty, 2);
    }
}

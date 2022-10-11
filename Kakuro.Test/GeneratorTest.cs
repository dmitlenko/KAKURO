using Kakuro.Engine;
using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;


namespace Kakuro.Test;

[TestClass]
public class GeneratorTest
{
    [TestMethod]
    public void Test4x4Easy()
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
    public void Test4x4Medium()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(4, 4, 2);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 4);
        Assert.AreEqual(b.Height, 4);
        Assert.AreEqual(b.Difficulty, 2);
    }

    [TestMethod]
    public void Test4x4Hard()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(4, 4, 3);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 4);
        Assert.AreEqual(b.Height, 4);
        Assert.AreEqual(b.Difficulty, 3);
    }

    [TestMethod]
    public void Test8x8Easy()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(8, 8, 1);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 8);
        Assert.AreEqual(b.Height, 8);
        Assert.AreEqual(b.Difficulty, 1);
    }

    [TestMethod]
    public void Test8x8Medium()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(8, 8, 2);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 8);
        Assert.AreEqual(b.Height, 8);
        Assert.AreEqual(b.Difficulty, 2);
    }

    [TestMethod]
    public void Test8x8Hard()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(8, 8, 3);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 8);
        Assert.AreEqual(b.Height, 8);
        Assert.AreEqual(b.Difficulty, 3);
    }

    [TestMethod]
    public void Test16x16Easy()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(16, 16, 1);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 16);
        Assert.AreEqual(b.Height, 16);
        Assert.AreEqual(b.Difficulty, 1);
    }

    [TestMethod]
    public void Test16x16Medium()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(16, 16, 2);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 16);
        Assert.AreEqual(b.Height, 16);
        Assert.AreEqual(b.Difficulty, 2);
    }

    [TestMethod]
    public void Test16x16Hard()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(16, 16, 3);

        Assert.IsTrue(s.Validate(b));
        Assert.AreEqual(b.Width, 16);
        Assert.AreEqual(b.Height, 16);
        Assert.AreEqual(b.Difficulty, 3);
    }
}

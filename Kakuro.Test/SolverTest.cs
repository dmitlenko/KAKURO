using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;

namespace Kakuro.Test;

[TestClass]
public class SolverTest
{
    Cell[,] grid5x5 = new Cell[,]
    {
        {new BlackCell(), new BlackCell(), new BlackCell(), new SumCell(-1,24), new SumCell(-1,7)},
        {new BlackCell(), new SumCell(-1,6), new SumCell(8,24), new WhiteCell(), new WhiteCell()},
        {new SumCell(18,-1), new WhiteCell(), new WhiteCell(), new WhiteCell(), new WhiteCell()},
        {new SumCell(23,-1), new WhiteCell(), new WhiteCell(), new WhiteCell(), new WhiteCell()},
        {new SumCell(12,-1), new WhiteCell(), new WhiteCell(), new BlackCell(), new BlackCell()},
    };

    Cell[,] grid3x3 = new Cell[,]
    {
        {new BlackCell(), new SumCell(-1,4), new SumCell(-1,1)},
        {new SumCell(4,-1), new WhiteCell(), new WhiteCell()},
        {new SumCell(5,-1), new WhiteCell(), new WhiteCell()}
    };

    Cell[,] grid13x13 = new Cell[,]
    {
        {new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new SumCell(-1,16),new SumCell(-1,3),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell()},
        {new BlackCell(),new SumCell(-1,6),new SumCell(-1,21),new BlackCell(),new SumCell(11,-1),new WhiteCell(),new WhiteCell(),new SumCell(-1,28),new BlackCell(),new SumCell(-1,29),new SumCell(-1,17),new BlackCell(),new BlackCell()},
        {new SumCell(4,-1),new WhiteCell(),new WhiteCell(),new BlackCell(),new SumCell(12, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new SumCell(14,7),new WhiteCell(),new WhiteCell(),new BlackCell(),new BlackCell() },
        {new SumCell(7,-1),new WhiteCell(),new WhiteCell(),new BlackCell(),new SumCell(-1, 16),new SumCell(-1, 4),new SumCell(26, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new BlackCell(),new BlackCell()},
        {new SumCell(3,-1),new WhiteCell(),new WhiteCell(),new SumCell(12,23),new WhiteCell(),new WhiteCell(),new SumCell(11,29),new WhiteCell(),new WhiteCell(),new WhiteCell(),new BlackCell(),new BlackCell(),new BlackCell() },
        {new BlackCell(),new SumCell(41,16),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new SumCell(-1,7),new SumCell(-1,38),new SumCell(-1,3)},
        {new SumCell(17, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new BlackCell(),new SumCell(15, -1),new WhiteCell(),new WhiteCell(),new BlackCell(),new SumCell(6, -1),new WhiteCell(),new WhiteCell(),new WhiteCell() },
        {new SumCell(21, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new SumCell(-1, 29),new SumCell(8, 23),new WhiteCell(),new WhiteCell(),new SumCell(-1,16),new SumCell(14,4),new WhiteCell(),new WhiteCell(),new WhiteCell() },
        {new BlackCell(),new BlackCell(),new BlackCell(),new SumCell(39, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new SumCell(-1,6)},
        {new BlackCell(),new BlackCell(),new BlackCell(),new SumCell(14,4),new WhiteCell(),new WhiteCell(),new WhiteCell(),new SumCell(8, -1),new WhiteCell(),new WhiteCell(),new SumCell(6, -1),new WhiteCell(),new WhiteCell()},
        {new BlackCell(),new BlackCell(),new SumCell(20, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new WhiteCell(),new SumCell(-1, 17),new SumCell(-1, 16),new BlackCell(),new SumCell(9, -1),new WhiteCell(),new WhiteCell() },
        {new BlackCell(),new BlackCell(),new SumCell(12, -1),new WhiteCell(),new WhiteCell(),new SumCell(21, -1),new WhiteCell(),new WhiteCell(),new WhiteCell(),new BlackCell(),new SumCell(10, -1),new WhiteCell(),new WhiteCell() },
        {new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell(),new SumCell(17, -1),new WhiteCell(),new WhiteCell(),new BlackCell(),new BlackCell(),new BlackCell(),new BlackCell() }
    };

    [TestMethod]
    public void Test5x5()
    {
        Solver s = new Solver();
        Assert.IsTrue(s.Validate(new KakuroBoard(grid5x5)));
    }

    [TestMethod]
    [ExpectedException(typeof(KakuroException))]
    public void Test3x3()
    {
        Solver s = new Solver();
        s.Validate(new KakuroBoard(grid3x3));
    }

    [TestMethod]
    public void Test13x13()
    {
        Solver s = new Solver();
        Assert.IsTrue(s.Validate(new KakuroBoard(grid13x13)));
    }
}
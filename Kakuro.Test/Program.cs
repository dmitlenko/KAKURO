using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Core;
using Kakuro.Engine.Cells;

class Program
{
    public static void Main()
    {
        Generator generator = new Generator();

        int w = 8;
        int h = 8;
        int d = 1;

        // Check validator
        KakuroBoard b = new KakuroBoard(5, 5);
        Solver s = new Solver();

        b.WhiteCells = 12;
        b.Solution = new Dictionary<string, int>(b.WhiteCells);
        b.Grid = new Cell[,]
        {
            { new BlackCell(), new BlackCell(), new BlackCell(), new SumCell(-1,24), new SumCell(-1,7) },
            { new BlackCell(), new SumCell(-1,6), new SumCell(8,24), new WhiteCell(), new WhiteCell() },
            { new SumCell(18,-1), new WhiteCell(), new WhiteCell(), new WhiteCell(), new WhiteCell() },
            { new SumCell(23,-1), new WhiteCell(), new WhiteCell(), new WhiteCell(), new WhiteCell() },
            { new SumCell(12,-1), new WhiteCell(), new WhiteCell(), new BlackCell(), new BlackCell() }
        };

        b.Solution = s.Solution(b);

        if (s.Validate(b)) Console.WriteLine("valid kakuro");
        else Console.WriteLine("not a valid kakuro");

        try
        {
            KakuroBoard board = generator.Generate(w, h, d);

            if (s.Validate(board)) Console.WriteLine("valid kakuro");
            else Console.WriteLine("not a valid kakuro");

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    string o = "";
                    if (board[i, j] is BlackCell)
                        o = "";
                    else if (board[i, j] is SumCell)
                        o = (board[i, j] as SumCell).ColSum + "\\" + (board[i, j] as SumCell).RowSum;
                    else if (board[i, j] is WhiteCell)
                        o = (board[i, j] as WhiteCell).Value.ToString();

                    Console.Write(String.Format("{0,8}", o));
                }
                Console.WriteLine();
            }

        }
        catch (KakuroException e)
        {
            Console.WriteLine(e.Message);
        }
    }
} 
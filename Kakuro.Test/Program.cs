using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;

class Program{
    static void Print(KakuroBoard b)
    {
        for(int i = 0; i < b.Height; i++)
        {
            for (int j = 0; j < b.Width; j++)
            {
                string o = "";
                if (b.Grid[i, j] is BlackCell)
                    o = "";
                else if (b.Grid[i, j] is SumCell)
                    o = (b.Grid[i, j] as SumCell).ColSum + "\\" + (b.Grid[i, j] as SumCell).RowSum;
                else if (b.Grid[i, j] is WhiteCell)
                    o = (b.Grid[i, j] as WhiteCell).Value.ToString();

                Console.Write(String.Format("{0,8}", o));
            }
            Console.WriteLine();
        }
    }

    public static void Main()
    {
        Solver s = new Solver();
        Generator g = new Generator();
        KakuroBoard b = g.Generate(6, 6, 2);

        Print(b);
    }
}
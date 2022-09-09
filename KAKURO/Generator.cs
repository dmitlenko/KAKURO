using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace KAKURO
{
    public class Cell
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public int HintV { get; set; }
        public int HintH { get; set; }
        public bool Border { get; set; }

        public Cell(string type)
        {
            Type = type;
        }

        public Cell(string type, bool border)
        {
            Type = type;
            Border = border;
        }
    }

    internal class Generator
    {
        private class Seen
        {
            public string IsSeen { get; set; }
            public Seen(string isSeen)
            {
                IsSeen = isSeen;
            }
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        private int RandomInt(int bound)
        {
            lock (syncLock)
            {
                return random.Next(bound);
            }
        }

        private double RandomDouble()
        {
            lock (syncLock)
            {
                return random.NextDouble();
            }
        }

        private Cell[,] Array2d(int width, int height, Func<int, int, Cell> fn)
        {
            Cell[,] cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    cells[y, x] = fn(x, y);
            return cells;
        }

        private int[][][] Map3dInt(Cell[,] arr, Func<int[]> fn)
        {
            int[][][] seenArr = new int[arr.GetLength(0)][][];
            for (int x = 0; x < arr.GetLength(0) - 1; x++)
            {
                seenArr[x] = new int[arr.GetLength(0)][];
                for (int y = 0; y < arr.GetLength(0) - 1; y++)
                {
                    seenArr[x][y] = fn();
                }  
            }
                
            return seenArr;
        }

        private bool[,] Map2dBool(Cell[,] arr, Func<bool> fn)
        {
            bool[,] seenArr = new bool[arr.GetLength(0), arr.GetLength(0)];
            for (int x = 0; x < arr.GetLength(0) - 1; x++)
                for (int y = 0; y < arr.GetLength(0) - 1; y++)
                    seenArr[y, x] = fn();

            return seenArr;
        }

        private void For2d(ref Cell[,] board, Func<int,int,Cell, Cell> fn)
        {
            for (int x = 0; x < board.GetLength(0) - 1; x++)
                for (int y = 0; y < board.GetLength(1) - 1; y++)
                    board[y, x] = fn(x,y,board[y, x]);
        }

        private void For2dType(ref Cell[,] board, string type, Func<int, int, Cell, Cell> fn)
        {
            For2d(ref board, (int x, int y, Cell cell) => {
                if(type == cell.Type) return fn(x,y,cell);
                return cell;
            });
        }

        private Tuple<int, int, int> ForNumberGroup(Cell[,] board, int x, int y, int xoff, int yoff, Func<int, int, Cell, Cell> fn, int len = 0)
        {
            x += xoff;
            y += yoff;
            Cell cell = board[y, x];
            if (cell.Type == "num")
            {
                fn(x, y, cell);
                return ForNumberGroup(board, x, y, xoff, yoff, fn, ++len);
            }
            return Tuple.Create(len, x, y);
        }

        private Cell[,] CalculateNumbers(Cell[,] board)
        {
            int[][][] seenrow = Map3dInt(board, () => null);
            int[][][] seencol = Map3dInt(board, () => null);

            For2dType(ref board, "hint", (x, y, cell) => {
                ForNumberGroup(board, x, y, 1, 0, (x1, y1, cell1) => {
                    seenrow[y1][x1] = new int[board.GetLength(0)];
                    return cell;
                });

                ForNumberGroup(board, x, y, 0, 1, (x1, y1, cell1) => {
                    seenrow[y1][x1] = new int[board.GetLength(0)];
                    return cell;
                });

                return cell;
            });

            For2dType(ref board, "num", (x, y, cell) => {
                int value = RandomInt(9) + 1;
                int origValue = value;

                int[] row = seenrow[y][x];
                int[] col = seencol[y][x];
                
                while ((row!= null && col != null) && ((row.Length > value || col.Length > value) || (row[value] != 0 || col[value] != 0)))
                {
                    value = (value % 9) + 1;
                    if (value == origValue) throw new Exception("number colision");
                }

                if (row != null) row[value] = 1;
                if (col != null) col[value] = 1;
                cell.Value = value;

                return cell;
            });

            For2dType(ref board, "hint", (x, y, cell) => {
                Func<int, int, int> TotalCells = (xoff, yoff) => {
                    int total = 0;

                    ForNumberGroup(board, x, y, xoff, yoff, (_x, _y, _cell) => {
                        total += cell.Value;
                        return cell;
                    });

                    return total;
                };

                cell.HintV = TotalCells(0, 1);
                cell.HintH = TotalCells(1, 0);

                return cell;
            });

            return board;
        }

        private void FillNeighbour(ref Cell[,] board, ref bool[,] seen, ref int seenCount, int _x, int _y)
        {
            Cell cell = board[_y, _x];

            if (cell.Type != "" && cell.Type == "num" && !seen[_y, _x])
            {
                seen[_y, _x] = true;
                ++seenCount;
                FillNeighbour(ref board, ref seen, ref seenCount, _x - 1, _y);
                FillNeighbour(ref board, ref seen, ref seenCount, _x + 1, _y);
                FillNeighbour(ref board, ref seen, ref seenCount, _x, _y - 1);
                FillNeighbour(ref board, ref seen, ref seenCount, _x, _y + 1);
            }
        }

        private Cell[,] FixBoard(Cell[,] board)
        {
            Random r = new Random();
            bool fixAgain;

            Func<int, int, bool> FixDir = (xoff, yoff) => {
                For2dType(ref board, "hint", (x, y, cell) => {
                    (int len, int ex, int ey) = ForNumberGroup(board, x, y, xoff, yoff, (_x, _y, _cell) => new Cell(""));
                    Cell endcell = board[ey, ex];

                    if (len == 1)
                    {
                        fixAgain = true;
                        if(endcell.Type != ""|| endcell.Border || RandomDouble() < 0.5 && !cell.Border)
                        {
                            cell.Type = "num";
                        } else
                        {
                            endcell.Type = "num";
                        }
                    } else if (len > 9)
                    {
                        Func<int, int, int> f = (a, b) => Convert.ToInt32(Math.Floor(a + (b - a) * RandomDouble()));
                        board[f(y + yoff, ey), f(x + xoff, ex)].Type = "hint";
                    }

                   return cell;
                });

                return true;
            };

            do
            {
                fixAgain = false;
                FixDir(0, 1);
                FixDir(1, 0);
            } while (fixAgain);

            int cx = 0, cy = 0;
            int cellCount = 0;

            bool[,] seen = Map2dBool(board, () => false);

            For2dType(ref board, "num", (x, y, cell) => {
                cx = x;
                cy = y;
                cellCount++;
                return cell;
            });

            int seenCount = 0;

            FillNeighbour(ref board, ref seen, ref seenCount, cx - 1, cy);
            FillNeighbour(ref board, ref seen, ref seenCount, cx + 1, cy);
            FillNeighbour(ref board, ref seen, ref seenCount, cx, cy - 1);
            FillNeighbour(ref board, ref seen, ref seenCount, cx, cy + 1);

            if (cellCount > seenCount)
            {
                throw new Exception("non-continuous board");
            } 

            return board;
        }


        private Cell[,] RandomBoard(int w, int h, double density)
        {
            Cell[,] board = Array2d(w + 2, h + 2, (x, y) => { 
                if(x == 0 || y == 0 || x == w+1 || y == h + 1)
                {
                    return new Cell("hint", true);
                } else
                {
                    return new Cell("num");
                }
            });

            Func<int,int,bool> IsHint = (x,y) => board[y, x].Type == "hint";

            Func<int, int, bool> IsValid = (x, y) => {
                var ints = new[] { (1,0), ( 0, 1 ), ( 0, -1 ), (-1, 0) };
                return Array.TrueForAll(ints, value => IsHint(x + value.Item1, y + value.Item2) || !IsHint(x + value.Item1 * 2, y + value.Item2 * 2));
            };

            for (int i = 0; i < (density * w * h);)
            {
                int x = RandomInt(w) + 1;
                int y = RandomInt(h) + 1;
                if(!IsHint(x,y) && IsValid(x, y)){
                    board[y, x].Type = "hint";
                    Debug.WriteLine(String.Format("valid x: {0} y: {1}", x, y));
                    i++;
                }
            }

            return board;
        }

        public Cell[,] GenerateBoard(int width, int height, double density)
        {
            Cell[,] board = new Cell[width,height];
            bool badBoard;

            do
            {
                try
                {
                    badBoard = false;
                    Debug.WriteLine("RandomBoard started");
                    board = RandomBoard(width, height, density);
                    Debug.WriteLine("RandomBoard done");

                    Debug.WriteLine("FixBoard started");
                    board = FixBoard(board);
                    Debug.WriteLine("FixBoard done");

                    Debug.WriteLine("CalculateNumbers started");
                    board = CalculateNumbers(board);
                    Debug.WriteLine("CalculateNumbers done");
                }
                catch (Exception)
                {
                    badBoard = true;
                }
                
            } while (badBoard);

            return board;
        }

        public GraphicTile[,] CellsToGraphicTiles(Cell[,] board)
        {
            GraphicTile[,] tiles = new GraphicTile[board.GetLength(0), board.GetLength(1)];
            for (int x = 0; x < board.GetLength(0); x++)
                for (int y = 0; y < board.GetLength(1); y++)
                    if (board[y, x].Type == "hint" && board[y, x].Border) tiles[y, x] = new BlackGraphicTile();
                    else if (board[y, x].Type == "hint" && !board[y,x].Border) tiles[y, x] = new HintGraphicTile(board[y, x].HintH, board[y, x].HintV);
                    else if (board[y, x].Type == "num") tiles[y, x] = new NumberGraphicTile();

            return tiles;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace KAKURO
{
    internal class BoardGenerator
    {
        private class GameCell
        {
            public string Type { get; set; }
            public int Value { get; set; }
            public int HintV { get; set; }
            public int HintH { get; set; }
            public bool Border { get; set; }

            public GameCell(string type)
            {
                Type = type;
            }

            public GameCell(string type, bool border)
            {
                Type = type;
                Border = border;
            }
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        private GameCell[,] Board;

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

        private GameCell[,] Array2d(int width, int height, Func<int, int, GameCell> fn)
        {
            GameCell[,] cells = new GameCell[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    cells[y, x] = fn(x, y);
            return cells;
        }

        private int[][][] Map3dInt(GameCell[,] arr, Func<int[]> fn)
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

        private bool[,] Map2dBool(GameCell[,] arr, Func<bool> fn)
        {
            bool[,] seenArr = new bool[arr.GetLength(0), arr.GetLength(0)];
            for (int x = 0; x < arr.GetLength(0) - 1; x++)
                for (int y = 0; y < arr.GetLength(0) - 1; y++)
                    seenArr[y, x] = fn();

            return seenArr;
        }

        private void For2d(ref GameCell[,] board, Func<int,int,GameCell, GameCell> fn)
        {
            for (int x = 0; x < board.GetLength(0) - 1; x++)
                for (int y = 0; y < board.GetLength(1) - 1; y++)
                    board[y, x] = fn(x,y,board[y, x]);
        }

        private void For2dType(ref GameCell[,] board, string type, Func<int, int, GameCell, GameCell> fn)
        {
            For2d(ref board, (int x, int y, GameCell cell) => {
                if(type == cell.Type) return fn(x,y,cell);
                return cell;
            });
        }

        private Tuple<int, int, int> ForNumberGroup(ref GameCell[,] board, int x, int y, int xoff, int yoff, Func<int, int, GameCell, GameCell> fn, int len = 0)
        {
            x += xoff;
            y += yoff;
            GameCell cell = board[y, x];
            if (cell.Type == "num")
            {
                fn(x, y, cell);
                return ForNumberGroup(ref board, x, y, xoff, yoff, fn, ++len);
            }
            return Tuple.Create(len, x, y);
        }

        private GameCell[,] CalculateNumbers(GameCell[,] board)
        {
            int[][][] seenrow = Map3dInt(board, () => null);
            int[][][] seencol = Map3dInt(board, () => null);

            For2dType(ref board, "hint", (x, y, cell) => {
                ForNumberGroup(ref board, x, y, 1, 0, (x1, y1, cell1) => {
                    seenrow[y1][x1] = new int[board.GetLength(0) + 2];
                    return cell;
                });

                ForNumberGroup(ref board, x, y, 0, 1, (x1, y1, cell1) => {
                    seenrow[y1][x1] = new int[board.GetLength(0) + 2];
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

                    ForNumberGroup(ref board, x, y, xoff, yoff, (_x, _y, _cell) => {
                        total += _cell.Value;
                        return _cell;
                    });

                    return total;
                };

                cell.HintV = TotalCells(0, 1);
                cell.HintH = TotalCells(1, 0);

                return cell;
            });

            return board;
        }

        private void FillNeighbour(ref GameCell[,] board, ref bool[,] seen, ref int seenCount, int _x, int _y)
        {
            GameCell cell = board[_y, _x];

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

        private GameCell[,] FixBoard(GameCell[,] board)
        {
            Random r = new Random();
            bool fixAgain;

            Func<int, int, bool> FixDir = (xoff, yoff) => {
                For2dType(ref board, "hint", (x, y, cell) => {
                    (int len, int ex, int ey) = ForNumberGroup(ref board, x, y, xoff, yoff, (_x, _y, _cell) => new GameCell(""));
                    GameCell endcell = board[ey, ex];

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


        private GameCell[,] RandomBoard(int w, int h, double density)
        {
            GameCell[,] board = Array2d(w + 2, h + 2, (x, y) => { 
                if(x == 0 || y == 0 || x == w+1 || y == h + 1)
                {
                    return new GameCell("hint", true);
                } else
                {
                    return new GameCell("num");
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
                    i++;
                }
            }

            return board;
        }

        private void CheckDuplicates(GameCell[,] board)
        {
            For2dType(ref board, "hint", (x, y, cell) => {
                Func<int, int, bool> Check = (xoff, yoff) => {
                    HashSet<int> hs = new HashSet<int>();

                    ForNumberGroup(ref board, x, y, xoff, yoff, (_x, _y, _cell) => {
                        if (!hs.Add(_cell.Value)) 
                            throw new Exception("found duplicates");
                        return _cell;
                    });

                    return true;
                };

                Check(0, 1);
                Check(1, 0);

                return cell;
            });
        }

        public void GenerateBoard(int width, int height, double density)
        {
            Board = new GameCell[width,height];
            bool badBoard;

            do
            {
                try
                {
                    badBoard = false;
                    Board = RandomBoard(width, height, density);
                    Board = FixBoard(Board);
                    Board = CalculateNumbers(Board);
                    CheckDuplicates(Board);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    badBoard = true;
                }
                
            } while (badBoard);
        }

        public Cell[,] Cells()
        {
            Cell[,] arr = new Cell[Board.GetLength(0), Board.GetLength(1)];

            for(int i = 0; i < Board.GetLength(0); i++)
                for(int j = 0; j < Board.GetLength(1); j++)
                    if (Board[i, j].HintH == 0 && Board[i, j].HintV == 0 && Board[i, j].Type != "num")
                        arr[i, j] = new BlackCell();
                    else if (Board[i, j].Type == "hint")
                        arr[i, j] = new HintCell(Board[i, j].HintV, Board[i, j].HintH);
                    else
                        arr[i, j] = new NumberCell();

            return arr;
        }

        public Cell[,] Solved()
        {
            Cell[,] arr = new Cell[Board.GetLength(0), Board.GetLength(1)];

            for (int i = 0; i < Board.GetLength(0); i++)
                for (int j = 0; j < Board.GetLength(1); j++)
                    if (Board[i, j].HintH == 0 && Board[i, j].HintV == 0 && Board[i, j].Type != "num")
                        arr[i, j] = new BlackCell();
                    else if (Board[i, j].Type == "hint")
                        arr[i, j] = new HintCell(Board[i, j].HintV, Board[i, j].HintH);
                    else
                        arr[i, j] = new NumberCell(Board[i, j].Value);

            return arr;
        }
    }
}

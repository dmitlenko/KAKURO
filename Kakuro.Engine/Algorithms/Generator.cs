using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kakuro.Engine.Algorithms
{
    public class Generator
    {
        private List<int> candidate_cells;
        private Dictionary<int, int> cell_index;
        private Dictionary<int, bool[]> cell_validity;
        private KakuroBoard k;
        private Random random = new Random();
        //private static readonly object syncLock = new object();

        /**
         * <summary>Default constructor for Generator</summary>
         */

        public Generator()
        {
            k = null;
            candidate_cells = null;
            cell_validity = null;
            cell_index = null;
        }

        /**
         * <summary>Generate valid kakuro board</summary>
         * <param name="width">Board width</param>
         * <param name="height">Board height</param>
         * <param name="difficulty">Board difficulty</param>
         * <returns>Returns kakuro board</returns>
         */

        public KakuroBoard Generate(int width, int height, int difficulty)
        {
            k = new KakuroBoard(width, height);
            k.Difficulty = difficulty;
            Solver solver = new Solver();
            int max_white_cells, max_kakuros = 169;

            if (difficulty == 1) max_white_cells = (int)Math.Ceiling(((k.Height - 1) * (k.Width - 1) * (NextDouble() * 15 + 15) / 100));
            else if (difficulty == 2) max_white_cells = (int)Math.Ceiling(((k.Height - 1) * (k.Width - 1) * (NextDouble() * 20 + 36) / 100));
            else max_white_cells = (int)Math.Ceiling(((k.Height - 1) * (k.Width - 1) * (NextDouble() * 25 + 61) / 100));

            generatenew:
            while (true)
            {
                while (true)
                {
                    InitializeGrid();
                    GenerateBlackCells(max_white_cells);
                    if ((CorrectGrid(true) && CorrectGrid(false))) break;
                }

                int act = 0;
                bool sum_cells_row_generated = false;
                Dictionary<int, int[]> sum_cell_info = new Dictionary<int, int[]>();

                while (act <= max_kakuros)
                {
                    while (act <= max_kakuros && !sum_cells_row_generated)
                    {
                        sum_cells_row_generated = GetSumCellsRow(ref sum_cell_info);
                        ++act;
                    }

                    if (sum_cells_row_generated)
                    {
                        GetSumCellsColumn(ref sum_cell_info);

                        try
                        {
                            solver.sum_cell_info = sum_cell_info;
                            if (solver.Validate(k)) goto outter;
                        }
                        catch (Exception)
                        {
                            goto generatenew;
                        }
                    }
                    sum_cells_row_generated = false;
                }
            }

        outter:
            // check again
            try
            {
                solver.sum_cell_info = null;
                solver.Validate(k);
            }
            catch (Exception)
            {
                goto generatenew;
            }

            KakuroBoard aux = k;
            k = null;
            candidate_cells = null;
            cell_validity = null;
            cell_index = null;
            return aux;
        }

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = NextInt(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void AddCandidateCell(int cell)
        {
            candidate_cells.Add(cell);
            cell_validity[cell][1] = true;
            cell_index[cell] = candidate_cells.Count - 1;
        }

        private bool CheckPattern(int cell)
        {
            int r = cell / k.Width;
            int c = cell % k.Height;

            if (r - 2 >= 0 && k.Grid[r - 1, c] is WhiteCell && k.Grid[r - 2, c] is BlackCell) return false;
            if (c - 2 >= 0 && k.Grid[r, c - 1] is WhiteCell && k.Grid[r, c - 2] is BlackCell) return false;
            if (r + 2 < k.Height && k.Grid[r + 1, c] is WhiteCell && k.Grid[r + 2, c] is BlackCell) return false;
            if (c + 2 < k.Width && k.Grid[r, c + 1] is WhiteCell && k.Grid[r, c + 2] is BlackCell) return false;
            if (r + 2 == k.Height && k.Grid[r + 1, c] is WhiteCell) return false;
            if (c + 2 == k.Width && k.Grid[r, c + 1] is WhiteCell) return false;

            return true;
        }

        private bool CorrectGrid(bool row)
        {
            int count, r, c, r_ini, c_ini, max_i, max_j, bound;
            r_ini = c_ini = count = 0;

            bound = GetStripBound();
            if (row)
            {
                max_i = k.Height;
                max_j = k.Width;
            }
            else
            {
                max_i = k.Width;
                max_j = k.Height;
            }

            for (int i = 1; i < max_i; i++)
            {
                for (int j = 1; j < max_j; j++)
                {
                    if (row)
                    {
                        r = i;
                        c = j;
                    }
                    else
                    {
                        r = j;
                        c = i;
                    }

                    if (k.Grid[r, c] is WhiteCell)
                    {
                        count++;
                        if (count == 1)
                        {
                            r_ini = r;
                            c_ini = c;
                        }
                        else if (count == bound)
                        {
                            bool painted = false;
                            int cell, next_black_cell = 0;

                            List<int> l;
                            if (row)
                                l = GetCandidatePositionList(bound, max_j);
                            else
                                l = GetCandidatePositionList(bound, max_i);

                            List<int>.Enumerator enumerator = l.GetEnumerator();

                            while (enumerator.MoveNext() && !painted)
                            {
                                next_black_cell = enumerator.Current;

                                if (row)
                                    cell = r_ini * k.Width + c_ini + next_black_cell;
                                else
                                    cell = (r_ini + next_black_cell) * k.Width + c_ini;

                                PaintCells(cell, true);
                                painted = CheckPattern(cell) && IsConnectedGrid();

                                if (!painted) PaintCells(cell, false);
                            }

                            if (!painted) return false;
                            else
                            {
                                if (row) j = c_ini + next_black_cell;
                                else j = r_ini + next_black_cell;
                            }

                            bound = GetStripBound();
                            count = 0;
                        }
                    }
                    else count = 0;
                }
                count = 0;
            }

            return true;
        }

        private int CountWhiteCells(bool[,] visited, int pos)
        {
            int count = 0;
            int r = pos / k.Width;
            int c = pos % k.Width;

            if (r >= 1 && c >= 1 && r < k.Height && c < k.Width && (k.Grid[r, c] is WhiteCell) && !visited[r, c])
            {
                visited[r, c] = true;
                ++count;
                count += CountWhiteCells(visited, pos + 1);
                count += CountWhiteCells(visited, pos - 1);
                count += CountWhiteCells(visited, pos + k.Width);
                count += CountWhiteCells(visited, pos - k.Width);
            }

            return count;
        }

        private int FillRow(int r, int c)
        {
            HashSet<int> repeated_row = new HashSet<int>();
            int j = c;
            int sum_row = 0;

            while (++j < k.Width && k.Grid[r, j] is WhiteCell)
            {
                //k.Solution[r.ToString() + j] = int.MinValue;
                HashSet<int> valid = new HashSet<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

                valid.RemoveWhere(value => repeated_row.Contains(value));
                if (!valid.Any()) return -1;

                int aux_i = r;
                while (--aux_i >= 0 && k.Grid[aux_i, j] is WhiteCell)
                    valid.Remove((k.Grid[aux_i, j] as WhiteCell).Value);

                if (!valid.Any()) return -1;

                int value = GetRandomSetValue(valid);
                (k.Grid[r, j] as WhiteCell).Value = value;
                repeated_row.Add(value);
                sum_row += value;
            }

            --j;
            (k.Grid[r, c] as SumCell).RowSum = sum_row;
            return j;
        }

        private void GenerateBlackCells(int max_white_cells)
        {
            candidate_cells = new List<int>((k.Height - 1) * (k.Width - 1));
            cell_validity = new Dictionary<int, bool[]>((k.Height - 1) * (k.Width - 1));
            cell_index = new Dictionary<int, int>((k.Height - 1) * (k.Width - 1));

            GetCandidateCells();
            int painted_cells, cell, cell_sim = 0;

            while (k.WhiteCells > max_white_cells && candidate_cells.Any())
            {
                cell = candidate_cells[NextInt(candidate_cells.Count)];
                painted_cells = PaintCells(cell, true);

                RemoveCandidateCell(cell, 0);

                if (Math.Abs(painted_cells) == 2)
                {
                    cell_sim = (k.Height - (cell / k.Width)) * k.Width + (k.Width - (cell % k.Width));
                    RemoveCandidateCell(cell_sim, 0);
                }

                if (IsConnectedGrid())
                {
                    UpdateCandidateCells(cell);
                    if (Math.Abs(painted_cells) == 2) UpdateCandidateCells(cell_sim);
                }
                else
                {
                    PaintCells(cell, false);
                }
            }
        }

        private void GetCandidateCells()
        {
            int array_pos, cell;
            bool[] constrains;
            array_pos = 0;

            for (int i = 1; i < k.Height; ++i)
            {
                for (int j = 1; j < k.Width; ++j)
                {
                    constrains = new bool[] { true, true };
                    cell = i * k.Width + j;

                    if (i != 2 && i != k.Height - 2 && j != 2 && j != k.Width - 2)
                    {
                        candidate_cells.Add(cell);
                        cell_index[cell] = array_pos++;
                    }
                    else
                    {
                        cell_index[cell] = -1;
                        constrains[1] = false;
                    }

                    cell_validity[cell] = constrains;
                }
            }
        }

        private List<int> GetCandidatePositionList(int bound, int max)
        {
            List<int> l1, l2, l3, l4;

            l1 = new List<int>(4);
            l2 = new List<int>(8);
            l3 = new List<int>(2);
            l4 = new List<int>(bound);

            l1.Add(0);
            for (int i = 2; i < 5 && i < max && i < bound; ++i) l1.Add(i);
            for (int i = 5; i < 8 && i < max && i < bound; ++i) l2.Add(i);
            for (int i = 8; i < 10 && i < max && i < bound; ++i) l3.Add(i);

            Shuffle(l1);
            Shuffle(l2);
            Shuffle(l3);

            if (bound == 5) l4.AddRange(l1);
            else if (bound == 8)
            {
                l4.AddRange(l2);
                l4.AddRange(l1);
            }
            else
            {
                l4.AddRange(l3);
                l4.AddRange(l2);
                l4.AddRange(l1);
            }

            return l4;
        }

        private int GetRandomSetValue(HashSet<int> s)
        {
            HashSet<int>.Enumerator it = s.GetEnumerator();
            if (s.Count == 1) return s.Single();
            int value = new Random((int)Time.UnixTimeMillis()).Next(s.Count);
            int i = 0;

            while (it.MoveNext())
            {
                if (i == value)
                {
                    value = it.Current;
                    break;
                }

                i++;
                it.MoveNext();
            }

            return value;
        }

        private int GetStripBound()
        {
            int bound, n = new Random((int)Time.UnixTimeMillis()).Next(10);

            if (k.Difficulty == 1)
            {
                if (n < 7) bound = 5;
                else bound = 8;
            }
            else if (k.Difficulty == 2)
            {
                if (n < 2) bound = 5;
                else if (n < 8) bound = 8;
                else bound = 10;
            }
            else
            {
                if (n < 7) bound = 10;
                else bound = 8;
            }

            return bound;
        }

        private void GetSumCellsColumn(ref Dictionary<int, int[]> sum_cell_info)
        {
            for (int j = 1; j < k.Width; ++j)
            {
                for (int i = 0; i < k.Height; ++i)
                {
                    if (k.Grid[i, j] is WhiteCell) (k.Grid[i, j] as WhiteCell).Value = 0;
                    else if (IsNextCellWhite(i, j, false))
                    {
                        if (!(k.Grid[i, j] is SumCell))
                        {
                            k.Grid[i, j] = new SumCell();
                            sum_cell_info[i * k.Width + j] = new int[] { 0, 0, 0, 0 };
                        }

                        int r = i;
                        int c = j;
                        int sum_column = 0;

                        while (++i < k.Height && k.Grid[i, j] is WhiteCell)
                        {
                            sum_column += (k.Grid[i, j] as WhiteCell).Value;
                            (k.Grid[i, j] as WhiteCell).Value = 0;
                        }

                        (k.Grid[r, c] as SumCell).ColSum = sum_column;
                        --i;

                        sum_cell_info[r * k.Width + c][1] = i - r;
                    }
                }
            }
        }

        private bool GetSumCellsRow(ref Dictionary<int, int[]> sum_cell_info)
        {
            k.Solution = new Dictionary<string, int>(k.WhiteCells);

            for (int i = 1; i < k.Height; ++i)
            {
                for (int j = 0; j < k.Width; ++j)
                {
                    if (IsNextCellWhite(i, j, true))
                    {
                        k.Grid[i, j] = new SumCell();
                        int next_j = FillRow(i, j);

                        if (j == -1) return false;
                        else
                        {
                            sum_cell_info[i * k.Width + j] = new int[] { next_j - j, 0, 0, 0 };
                            j = next_j;
                        }
                    }
                }
            }

            return true;
        }

        private void InitializeGrid()
        {
            k.Grid = new Cell[k.Height, k.Width];
            k.WhiteCells = (k.Height - 1) * (k.Width - 1);

            for (int i = 0; i < k.Height; i++)
            {
                for (int j = 0; j < k.Width; j++)
                {
                    if (i == 0 || j == 0) k.Grid[i, j] = new BlackCell();
                    else k.Grid[i, j] = new WhiteCell();
                }
            }
        }

        private bool InvalidPatternCell(int cell) => cell_validity[cell][0] && !cell_validity[cell][1];

        private bool IsConnectedGrid()
        {
            int pos = NextWhiteCellPos(k, k.Width + 1);
            if (pos >= k.Width * k.Height) return false;

            bool[,] visited = new bool[k.Height, k.Width];
            for (int i = 0; i < k.Height; i++)
            {
                for (int j = 0; j < k.Width; j++) visited[i, j] = !(k.Grid[i, j] is WhiteCell);
            }

            return CountWhiteCells(visited, pos) == k.WhiteCells;
        }

        private bool IsNextCellWhite(int r, int c, bool is_row)
        {
            if (is_row) return (++c < k.Width && k.Grid[r, c] is WhiteCell);
            return (++r < k.Height && k.Grid[r, c] is WhiteCell);
        }

        private double NextDouble()
        {
            return random.NextDouble();
        }

        private int NextInt(int to)
        {
            return random.Next(to);
        }
        private int NextWhiteCellPos(KakuroBoard k, int pos)
        {
            int r = pos / k.Width;
            int c = pos % k.Width;

            while (r < k.Height && c < k.Width && !(k.Grid[r, c] is WhiteCell))
            {
                pos++;
                r = pos / k.Width;
                c = pos % k.Width;
            }

            return pos;
        }

        private int PaintCells(int cell, bool black)
        {
            int r, c, r_sim, c_sim, painted_cells;
            r = cell / k.Width;
            c = cell % k.Width;
            r_sim = k.Height - r;
            c_sim = k.Width - c;

            if (black)
            {
                painted_cells = -1;
                k.Grid[r, c] = new BlackCell();
            }
            else
            {
                painted_cells = 1;
                k.Grid[r, c] = new WhiteCell();
            }

            if (r != r_sim || c != c_sim)
            {
                if (black)
                {
                    k.Grid[r_sim, c_sim] = new BlackCell();
                    painted_cells--;
                }
                else
                {
                    k.Grid[r_sim, c_sim] = new WhiteCell();
                    painted_cells++;
                }
            }

            k.WhiteCells += painted_cells;
            return painted_cells;
        }

        private void RemoveCandidateCell(int cell, int constraint)
        {
            int index = cell_index[cell];
            if (index != -1)
            {
                Swap(ref candidate_cells, index, candidate_cells.Count - 1);
                cell_index[candidate_cells[index]] = index;
                cell_index[cell] = -1;
                cell_validity[cell][constraint] = false;
                candidate_cells.RemoveAt(candidate_cells.Count - 1);
            }
        }

        private void Swap<T>(ref List<T> list, int i, int j)
        {
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }

        private void UpdateCandidateCells(int cell)
        {
            int r = cell / k.Width;
            int c = cell % k.Width;

            if (c + 2 < k.Width && ValidCell(cell + 2)) RemoveCandidateCell(cell + 2, 1);
            if (c - 2 >= 1 && ValidCell(cell - 2)) RemoveCandidateCell(cell - 2, 1);
            if (r + 2 < k.Height && ValidCell(cell + 2 * k.Width)) RemoveCandidateCell(cell + 2 * k.Width, 1);
            if (r - 2 >= 1 && ValidCell(cell - 2 * k.Width)) RemoveCandidateCell(cell - 2 * k.Width, 1);
            if (c + 1 < k.Width && InvalidPatternCell(cell + 1) && CheckPattern(cell + 1)) AddCandidateCell(cell + 1);
            if (c - 1 >= 1 && InvalidPatternCell(cell - 1) && CheckPattern(cell - 1)) AddCandidateCell(cell - 1);
            if (r + 1 < k.Height && InvalidPatternCell(cell + k.Width) && CheckPattern(cell + k.Width)) AddCandidateCell(cell + k.Width);
            if (r - 1 >= 1 && InvalidPatternCell(cell - k.Width) && CheckPattern(cell - k.Width)) AddCandidateCell(cell - k.Width);
        }
        private bool ValidCell(int cell) => cell_validity[cell][0] && cell_validity[cell][1];
    }
}
using Kakuro.Engine.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Algorithms
{
    public class Solver
    {
        private static Combos comb = null;
        private KakuroBoard k = null;
        private Dictionary<int, HashSet<int>> white_cell_values = null;
        private Dictionary<int, int[]> white_cell_info = null;
        public Dictionary<int, int[]> sum_cell_info = null;

        /**
         * <summary>Default static constructor for the Solver</summary>
         */
        static Solver() => comb = Combos.Instance;

        /**
         * <summary>Validates Kakuro board</summary>
         * <param name="board">Kakuro board</param>
         * <returns>Returns <c>true</c> if board is valid</returns>
         */
        public bool Validate(KakuroBoard board)
        {
            k = board;
            bool[] solution = new bool[] { false, false };

            if(sum_cell_info == null)
            {
                sum_cell_info = new Dictionary<int, int[]>();
                CheckSumCells();
            }

            white_cell_info = new Dictionary<int, int[]>(k.WhiteCells);
            white_cell_values = new Dictionary<int, HashSet<int>>(k.WhiteCells);

            InitGridInfo(true, k.Height, k.Width);
            InitGridInfo(false, k.Width, k.Height);

            int cell = GetNextWhiteCellPos(1, 0);
            List<int> cells_to_empty = new List<int>();

            SolveClues(cell, ref cells_to_empty);
            SolveGrid(cell, ref solution);
            FreeMemory();

            if (!solution[0]) throw new KakuroException("Kakuro without solution");
            if (!solution[1]) throw new KakuroException("Kakuro with more than 1 solution");
            for (int i = 0; i < cells_to_empty.Count; i++)
                k[cells_to_empty[i] / k.Width, cells_to_empty[i] % k.Width].Value = 0;
            return true;
        }

        private void FreeMemory()
        {
            k = null;
            white_cell_values = null;
            white_cell_info = null;
            sum_cell_info = null;
        }

        private void CheckSumCells()
        {
            for(int i = 0; i < k.Height; i++)
            {
                for(int j = 0; j < k.Width; j++)
                {
                    if (k[i,j] is SumCell)
                    {
                        sum_cell_info[i * k.Width + j] = new[] { 0, 0, 0, 0 };
                        if (k[i, j].HasColSum) CheckSumStride(i, j, 1, 0, k[i, j].ColSum);
                        if (k[i, j].HasRowSum) CheckSumStride(i, j, 0, 1, k[i, j].RowSum);
                    }
                }
            }
        }

        private int CheckSumStride(int row, int col, int i, int j, int sum)
        {
            (int r_ini, int c_ini) = (row, col);
            int num_cells = 0;

            row += i;
            col += j;
            while (row < k.Height && col < k.Width && k[row, col].GetType() == typeof(WhiteCell))
            {
                num_cells++;
                row += i;
                col += j;
            }

            if (!comb.ExistsSumOfCells(sum, num_cells))
            {
                FreeMemory();
                throw new KakuroException("Kakuro with impossible sum in strides");
            }

            sum_cell_info[r_ini * k.Height + c_ini][i] = num_cells;
            return num_cells;
        }

        private void InitGridInfo(bool row, int max_i, int max_j)
        {
            int r, c;

            for(int i = 0; i < max_i; i++)
            {
                for(int j = 0; j < max_j; j++)
                {
                    if (row)
                    {
                        r = i;
                        c = j;
                    } else
                    {
                        r = j;
                        c = i;
                    }

                    if (k[r,c] is SumCell)
                    {
                        if (row) j = InitStrideInfo(r, c, max_i, max_j, 0, 1);
                        else j = InitStrideInfo(r, c, max_j, max_i, 1, 0);
                    }
                }
            }
        }

        private int InitStrideInfo(int row, int col, int max_r, int max_c, int i, int j)
        {
            int r_ini = row;
            int c_ini = col;

            row += i;
            col += j;

            while(row < max_r && col < max_c && k[row,col] is WhiteCell)
            {
                int cell = row * k.Width + col;
                if(i == 0)
                {
                    white_cell_info[cell] = new[] { r_ini * k.Width + c_ini, 0, GetNextWhiteCellPos(row, col) };
                    white_cell_values[cell] = comb.PossibleValues(sum_cell_info[r_ini * k.Width + c_ini][0], k[r_ini, c_ini].RowSum);
                } else
                {
                    int sum_cell = r_ini * k.Width + c_ini;
                    white_cell_info[cell][1] = sum_cell;
                    white_cell_values[cell].RemoveWhere(value => comb.PossibleValues(sum_cell_info[sum_cell][1], k[r_ini, c_ini].ColSum).Contains(value));
                }
                row += i;
                col += j;
            }
            if (i == 0) return --col;
            return --row;
        }

        private int GetNextWhiteCellPos(int row, int col)
        {
            for(int i = row; i < k.Height; i++)
            {
                for(int j = col; j < k.Width; j++)
                {
                    if (k[i, j] is WhiteCell) return row * k.Width + col;
                }
            }

            return 0;
        }

        private void SolveClues(int cell, ref List<int> cells_to_empty)
        {
            int bound = k.Width * k.Height;

            while (cell < bound)
            {
                int i = cell / k.Width;
                int j = cell % k.Width;

                if (k[i, j].IsUnassigned)
                {
                    HashSet<int> values = white_cell_values[cell];
                    if (!values.Any()) throw new KakuroException("Kakuro without solution");
                    else if (values.Count == 1)
                    {
                        HashSet<int>.Enumerator e = values.GetEnumerator(); e.MoveNext();
                        int value = e.Current;
                        k[i, j].Value = value;
                        cells_to_empty.Add(cell);
                        sum_cell_info[white_cell_info[cell][0]][2] += value;
                        sum_cell_info[white_cell_info[cell][1]][3] += value;
                        k.Solution[i.ToString() + j] = value;
                        values.Remove(value);
                        cell = UpdateStriders(cell, value);
                    }
                    else cell = white_cell_info[cell][2];
                }
                else cell = white_cell_info[cell][2];
            }
        }

        private int UpdateStriders(int cell, int value)
        {
            int next_cell_row, next_cell_col;

            next_cell_row = UpdateStridersValues(cell - 1, value, 0, -1);
            next_cell_col = UpdateStridersValues(cell - k.Width, value, -1, 0);

            UpdateStridersValues(cell + 1, value, 0, 1);
            UpdateStridersValues(cell + k.Width, value, 1, 0);

            if (next_cell_col != -1) return next_cell_col;
            else if (next_cell_row != -1) return next_cell_row;
            else return white_cell_info[cell][2];
        }

        private int UpdateStridersValues(int cell, int value, int inci, int incj)
        {
            int next_cell = -1;
            int i = cell / k.Width;
            int j = cell % k.Width;

            while (i >= 1 && j >= 1 && i < k.Height && j < k.Width && k[i, j] is WhiteCell)
            {
                cell = i * k.Width + j;
                if (k[i, j].IsUnassigned)
                {
                    HashSet<int> values = white_cell_values[cell];
                    values.Remove(value);
                    if (!values.Any()) throw new KakuroException("Kakuro without solution");
                    else if (values.Count == 1) next_cell = cell;
                }
                i += inci;
                j += incj;
            }

            return next_cell;
        }

        private bool SolveGrid(int cell, ref bool[] solution)
        {
            if(cell >= k.Width * k.Height)
            {
                if (!solution[0])
                {
                    solution[0] = solution[1] = true;
                    return false;
                } else
                {
                    solution[1] = false;
                    return true;
                }
            }
            else
            {
                int i = cell / k.Width;
                int j = cell % k.Width;
                int[] info_cell = white_cell_info[cell];
                int[] info_sum_cell_row = sum_cell_info[info_cell[0]];
                int[] info_sum_cell_column = sum_cell_info[info_cell[1]];

                if (!k[i, j].IsUnassigned)
                {
                    if (ValidValue(i, j, info_sum_cell_row[2], info_cell[0], true) &&
                        ValidValue(i, j, info_sum_cell_column[3], info_cell[1], false))
                    {
                        if (SolveGrid(info_cell[2], ref solution))
                        {
                            UpdateAcumulatedSums(info_sum_cell_row, info_sum_cell_column, -k[i, j].Value);
                            return true;
                        }
                    }
                    else return false;
                }
                else
                {
                    HashSet<int> values = white_cell_values[cell];
                    HashSet<int>.Enumerator it = values.GetEnumerator();
                    while(it.MoveNext()){
                        HashSet<int> cells_update_row = new HashSet<int>();
                        HashSet<int> cells_update_col = new HashSet<int>();
                        int value = it.Current;
                        UpdateAcumulatedSums(info_sum_cell_row, info_sum_cell_column, value);

                        if (ValidValue(i, j, info_sum_cell_row[2], info_cell[0], true) &&
                            ValidValue(i, j, info_sum_cell_column[3], info_cell[1], false))
                        {
                            if (CheckPruning(cells_update_row, i, j, 0, 1, value, info_sum_cell_row[2], info_cell[0]) &&
                                CheckPruning(cells_update_col, i, j, 1, 0, value, info_sum_cell_column[3], info_cell[1]))
                            {
                                k[i,j].Value = value;

                                if (!solution[0]) k.Solution[i.ToString() + j] = value;
                                if (SolveGrid(info_cell[2], ref solution))
                                {
                                    UpdateAcumulatedSums(info_sum_cell_row, info_sum_cell_column, -value);
                                    return true;
                                }
                                else k[i, j].Value = 0;
                            }
                        }

                        UpdateAcumulatedSums(info_sum_cell_row, info_sum_cell_column, -value);
                        UndoChanges(cells_update_row, value);
                        UndoChanges(cells_update_col, value);
                    }
                }
                return false;
            }
        }

        private void UpdateAcumulatedSums(int[] info_sum_cell_row, int[] info_sum_cell_column, int value)
        {
            info_sum_cell_row[2] += value;
            info_sum_cell_column[3] += value;
        }

        private bool ValidValue(int row, int col, int sum_act, int sum_cell, bool is_row)
        {
            bool is_next_cell_white, exists_unassigned;
            int sum_max;

            if (is_row)
            {
                is_next_cell_white = ++col < k.Width && k[row, col] is WhiteCell;
                sum_max = k[sum_cell / k.Width, sum_cell % k.Width].RowSum;
                exists_unassigned = CheckStrideValues(row, col + 1, 0, 1);
            }
            else
            {
                is_next_cell_white = ++row < k.Height && k[row, col] is WhiteCell;
                sum_max = k[sum_cell / k.Width, sum_cell % k.Width].ColSum;
                exists_unassigned = CheckStrideValues(row + 1, col, 1, 0);
            }

            if (!is_next_cell_white) return (sum_act == sum_max);
            else
            {
                if (exists_unassigned) return sum_act < sum_max;
                else return sum_act <= sum_max;
            }
        }

        private bool CheckStrideValues(int i, int j, int inci, int incj)
        {
            while(i < k.Height && j < k.Width && k[i,j] is WhiteCell)
            {
                if (k[i, j].IsUnassigned) return true;
                i += inci;
                j += incj;
            }
            return false;
        }

        private bool CheckPruning(HashSet<int> cells_update, int row, int col, int i, int j, int value, int sum_act, int sum_cell)
        {
            HashSet<int> pruning_values = new HashSet<int>();
            int count = 0;

            row += i;
            col += j;
            while(row < k.Height && col < k.Width && k[i,j] is WhiteCell)
            {
                if(k[i, j].IsUnassigned)
                {
                    HashSet<int> values = white_cell_values[row * k.Width + col];
                    count++;
                    if (values.Remove(value))
                    {
                        cells_update.Add(row * k.Width + col);
                        if (values.Count == 0) return false;
                    }
                    pruning_values.UnionWith(values);
                }
                row += i;
                col += j;
            }

            int[] aux = pruning_values.ToArray();
            Array.Sort(aux);

            int sum_max;
            if (i == 0) sum_max = k[sum_cell / k.Width, sum_cell % k.Width].RowSum;
            else sum_max = k[sum_cell / k.Width,sum_cell % k.Width].ColSum;

            if (count == 1 && !pruning_values.Contains(sum_max - sum_act)) return false;
            else
            {
                int max = 0;
                int min = 0;

                for(int k = 0; k < count && k < pruning_values.Count; k++)
                {
                    min += aux[k];
                    max += aux[pruning_values.Count - k - 1];
                }

                if (max + sum_act < sum_max) return false;
                if (min + sum_act > sum_max) return false;
            }

            return true;
        }

        private void UndoChanges(HashSet<int> cells_update, int value)
        {
            HashSet<int>.Enumerator it = cells_update.GetEnumerator();
            while (it.MoveNext()) white_cell_values[it.Current].Add(value);
        }
    }
}

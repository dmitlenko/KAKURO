using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kakuro.Engine.Cells;

namespace Kakuro.Engine
{
    public class KakuroBoard
    {
        /**
        * <summary>Name of board</summary>
        */
        public string Name { get; set; }

        /**
         * <summary>Number that represents difficulty of the board </summary>
         */
        public int Difficulty { get; set; }

        /**
         * <summary>Number that represents amount off cells in one row</summary>
         */
        public int Width { get; set; }

        /**
         * <summary>Number that represents amount of rows</summary>
         */
        public int Height { get; set; }

        /**
         * <summary>Number that represents amount of white cells on the board</summary>
         */
        public int WhiteCells { get; set; }

        public int CellsInitValue { get; private set; }

        /**
         * <summary>The matrix to hold all the cells of the Kakuro</summary>
         */
        public Cell[,] Grid;

        /**
         * <summary>Dictionary(K,V) that represents solution for the board</summary>
         * <param name="K">Represents coords on the board</param>
         * <param name="V">Represents value of the cell on the board</param>
         */
        public Dictionary<string, int> Solution { get; set; }

        /**
         * <summary>Create new kakuro board</summary>
         * <param name="width">Board width</param>
         * <param name="height">Board height</param>
         */
        public KakuroBoard(int width, int height)
        {
            Name = null;
            Width = width;
            Height = height;
            Difficulty = 0;
            WhiteCells = 0;
            Grid = null;
            Solution = null;
        }

        /**
         * <summary>Get element by its coords</summary>
         * <param name="row">Board row</param>
         * <param name="col">Board column</param>
         */
        public Cell this[int row, int col]
        {
            get => Grid == null ? new BlackCell() : Grid[row, col];
            set => Grid[row, col] = value;
        }

        /**
         * <summary>Give a hint of the square with row and column of the Kakuro</summary>
         * <param name="row">The row of the board</param>
         * <param name="col">The column of the board</param>
         * <returns>Returns the correct number in position row and col; if the position is incorrect, it returns -1</returns>
        */
        public int GetHelp(int row, int col)
        {
            if(row >= 0 && row < Width && col >= 0 && col < Height && Grid[row, col] is WhiteCell)
            {
                return Solution[String.Format("{0}{1}", row, col)];
            } else
            {
                return -1;
            }
        }

        /**
         * <summary>Assign value to a white box</summary>
         * <param name="row">The row of the board</param>
         * <param name="col">The column of the board</param>
         * <returns>Returns <c>true</c> if it could be modified, otherwise returns <c>false</c></returns>
        */
        public bool SetResult(int row, int col, int value)
        {
            if (CorrectValue(row, col, value))
            {
                if (Grid[row, col].IsUnassigned)
                {
                    Grid[row, col].Value = value;
                    return true;
                }

                Grid[row, col].Value = value;
            }

            return false;
        }

        /**
         * <summary>Removes value of the white box</summary>
         * <param name="row">The row of the board</param>
         * <param name="col">The column of the board</param>
        */
        public void DelResult(int row, int col)
        {
            Grid[row, col].Value = 0;
        }

        /**
         * <summary>Check that the value in a position is correct</summary>
         * <param name="row">The row of the board</param>
         * <param name="col">The column of the board</param>
         * <param name="value">The value to modify</param>
         * <returns>Returns whether it is correct or not</returns>
        */
        private bool CorrectValue(int row, int col, int value)
        {
            int nullref = 0;

            if(row >= Width || col >= Height || row < 0 || col < 0 || !(Grid[row, col] is WhiteCell))
                throw new KakuroException("Incorect row or column");

            if (value < 0 || value > 9)
                throw new KakuroException("The value must be between 0 and 9");

            bool full_up = true, full_down = true;
            int sum_up, sum_down, sum_act, sum_full = 0;

            sum_up = GetSum(row, col, value, true, -1, ref full_up, ref sum_full);
            if(sum_up == -1)
                throw new KakuroException("Column repeat value");

            sum_down = GetSum(row, col, value, false, 1, ref full_down, ref nullref);
            if (sum_down == -1)
                throw new KakuroException("Column repeat value");

            sum_act = sum_up + sum_down + value;
            if (sum_act > sum_full)
                throw new KakuroException("Column sum exceeded");

            if (sum_act == sum_full && !(full_up && full_down))
                throw new KakuroException("Column incomplete with equal sum");

            if ((full_up && full_down) && sum_act < sum_full)
                throw new KakuroException("Column sum fell short");

            return true;
        }

        /**
         * <summary>Get the result that the sum of a row or column should have</summary>
         * <param name="row">The row of the board</param>
         * <param name="col">The column of the board</param>
         * <param name="isrow">Search by row</param>
         * <returns>Returns the result that the sum of the row or column should have</returns>
        */
        private int GetSum(int row, int col, int value, bool isrow, int sign, ref bool is_full, ref int sum_full)
        {
            int i, j, sum = 0;

            if (isrow)
            {
                i = 0;
                j = 1 * sign;
            }
            else
            {
                i = 1 * sign;
                j = 0;
            }

            row += i;
            col += j;
            while(row >= 0 && col >= 0 && row < Height && col < Width && Grid[row,col] is WhiteCell)
            {
                if (Grid[row, col].IsUnassigned) is_full = false;
                else
                {
                    if (Grid[row, col].Value == value) return -1;
                    else sum += Grid[row, col].Value;
                }

                row += i;
                col += j;
            }

            if(sign == -1)
            {
                if (isrow) sum_full = Grid[row, col].RowSum;
                else sum_full = Grid[row, col].ColSum;
            }

            return sum;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Cells
{
    /**
     * <summary>Class to represent sum cell</summary>
     */
    public class SumCell : Cell
    {
        /**
         * <summary>SumCell's row sum</summary> 
         */
        public new int RowSum { get; set; }

        /**
         * <summary>SumCell's column sum</summary> 
         */
        public new int ColSum { get; set; }

        /**
         * <summary>Checks if SumCell has row sum</summary> 
         */
        public new bool HasRowSum { get => RowSum != -1; }

        /**
         * <summary>Checks if SumCell has column sum</summary> 
         */
        public new bool HasColSum { get => ColSum != -1; }

        /**
         * <summary>Constructor for SumCell class</summary>
         */
        public SumCell()
        {
            RowSum = ColSum = -1;
        }
    }
}

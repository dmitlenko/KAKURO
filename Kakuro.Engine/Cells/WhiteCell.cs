using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Cells
{
    /**
     * <summary>Class to represent white cell</summary>
     */
    public class WhiteCell: Cell
    {
        /**
         * <summary>Value of WhiteCell</summary> 
         */
        public new int Value { get; set; }

        /**
         * <summary>Is the white box unassigned</summary>
         */
        public new bool IsUnassigned { get => Value == 0; }

        /**
         * <summary>Constructor for WhiteCell class</summary>
         */
        public WhiteCell()
        {
            Value = 0;
        }

        /// <summary>
        /// Constructor for WhiteCell class
        /// </summary>
        /// <param name="value">Value of the white cell</param>
        public WhiteCell(int value)
        {
            Value = value;
        }
    }
}

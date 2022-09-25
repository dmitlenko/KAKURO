using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Cells
{
    /**
     * <summary>Abstract class to represent a Cell</summary> 
     */
    public abstract class Cell
    {
        /**
         * <summary>Default getter and setter for Cell value</summary>
         */
        public int Value { get => -1; set => _ = value; }

        /**
         * <summary>Default getter and setter for Cell's horizontal sum</summary>
         */
        public int RowSum { get => -1; set => _ = value; }

        /**
         * <summary>Default getter and setter for Cell's vertical sum</summary>
         */
        public int ColSum { get => -1; set => _ = value; }

        /**
         * <summary>Know if Cell is unassigned, that is, equal to 0</summary>
         */
        public bool IsUnassigned { get => false; }

        /**
         * <summary>Is cell have horizontal sum</summary>
         */
        public bool HasRowSum { get => false; }

        /**
         * <summary>Is cell have vertical sum</summary>
         */
        public bool HasColSum { get => false; }
    }

}

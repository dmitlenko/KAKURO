using Kakuro.Engine.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine
{
    [Serializable]
    public class GameSave
    {
        public Cell[,] Cells { get; } = null;
        public DateTime Time { get; } = new DateTime();

        public Size Size { get; } = Size.Empty;
        public Point Selection { get; } = Point.Empty;

        public GameSave(Cell[,] cells, DateTime time, Size size, Point selection)
        {
            this.Cells = cells;
            this.Time = time;
            Size = size;
            Selection = selection;
        }
    }
}

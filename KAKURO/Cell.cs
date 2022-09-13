using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAKURO
{
    [Serializable]
    public class Cell
    {
        public string Type { get; }

        public Cell() => Type = "cell";

        public Cell(string type) => Type = type;
    }

    [Serializable]
    public class BlackCell : Cell
    {
        public BlackCell(): base("black") { }
    }

    [Serializable]
    public class HintCell : Cell
    {
        public int HorizontalSum { get; } = 0;
        public int VerticalSum { get; } = 0;

        public HintCell(): base("hint") { }
        public HintCell(int verticalSum,int horizontalSum): base("hint")
        {
            HorizontalSum = horizontalSum;
            VerticalSum = verticalSum;
        }
    }

    [Serializable]
    public class NumberCell: Cell
    {
        public int Number { get; set; } = 0;

        public NumberCell(): base("number") { }
        public NumberCell(int number): base("number") => Number = number;
    }
}

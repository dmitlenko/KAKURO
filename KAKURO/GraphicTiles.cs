using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    public struct TileTypes
    {
        public static TileType Empty { get => new TileType("empty"); }
        public static TileType Black { get => new TileType("black"); }
        public static TileType Hint { get => new TileType("hint"); }
        public static TileType Number { get => new TileType("number"); }
    }

    public class TileType
    {
        public string Type { get; }

        public TileType(string type) => Type = type;

        public override string ToString() => Type;
    }

    internal class GraphicTile
    {
        //public PictureBox canvas { get; set; }
        public Bitmap buffer { get; set; }
        public TileType Type { get; }

        public Point Position;
        public Size Size;

        public bool Selected { get; set; }

        public GraphicTile() { Type = TileTypes.Empty; Selected = false; Position = Point.Empty; Size = Size.Empty; }
        public GraphicTile(TileType type) { /*Canvas = null;*/ Type = type; Selected = false; Position = Point.Empty; Size = Size.Empty; }
        public GraphicTile(PictureBox pb, TileType type) { /*Canvas = pb;*/ Type = type; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        public GraphicTile(PictureBox pb) { /*Canvas = pb;*/ Type = TileTypes.Empty; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        public virtual void Draw(Graphics ctx) { }
    }

    class BlackGraphicTile : GraphicTile
    {
        public BlackGraphicTile(PictureBox pictureBox) : base(pictureBox, TileTypes.Black) { }
        public BlackGraphicTile() : base(TileTypes.Black) { }

        public override void Draw(Graphics graphics) {
            //g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(Position, Size));

            Pen pen = new Pen(Color.Gray, 1);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Position, Size));
        }
    }

    class HintGraphicTile : GraphicTile
    {
        public int SumLeft {get;set;}
        public int SumRight {get;set;}

        public HintGraphicTile() : base(TileTypes.Hint)
        {
            SumLeft = 0;
            SumRight = 0;
        }

        public HintGraphicTile(int sumLeft, int sumRight) : base(TileTypes.Hint)
        {
            SumLeft = sumLeft;
            SumRight = sumRight;
        }

        public HintGraphicTile(PictureBox pictureBox, int sumLeft, int sumRight): base(pictureBox, TileTypes.Hint) 
        { 
            SumLeft = sumLeft;
            SumRight = sumRight;
        }

        private Tuple<int,int> GetStringWidth(string str, Font font)
        {
            SizeF stringSize = new SizeF();
            Graphics gfx = Graphics.FromImage(new Bitmap(1, 1));
            stringSize = gfx.MeasureString(str, font);
            return Tuple.Create((int)stringSize.Width, (int)stringSize.Height);
        }

        public override void Draw(Graphics graphics)
        {
            //g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(Position, Size));
            graphics.DrawLine(new Pen(Color.White, 2), Position, Point.Add(Position, Size));

            int padding = 2;
            int fontSize = 16;
            Font drawFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            graphics.DrawString(SumLeft == 0 ? "" : SumLeft.ToString(), drawFont, Brushes.White, Position.X + padding, Size.Height - GetStringWidth(SumRight.ToString(), drawFont).Item2);
            graphics.DrawString(SumRight == 0 ? "" : SumRight.ToString(), drawFont, Brushes.White, Size.Width - GetStringWidth(SumRight.ToString(), drawFont).Item1, Size.Height + padding);

            if (Selected)
            {
                Pen pen = new Pen(Color.Gray, 1);
                pen.Alignment = PenAlignment.Inset;
                graphics.DrawRectangle(pen, new Rectangle(Position, Size));
            }
        }
    }

    class NumberGraphicTile : GraphicTile
    {
        private int DrawnNumber { get; set; }

        public NumberGraphicTile(): base(TileTypes.Number) { DrawnNumber = 0; }

        public NumberGraphicTile(int drawnNumber) : base(TileTypes.Number)
        {
            DrawnNumber = drawnNumber;
        }

        public NumberGraphicTile(PictureBox pictureBox, int drawnNumber) : base(pictureBox, TileTypes.Number)
        {
            DrawnNumber = drawnNumber;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, new Rectangle(Point.Add(Position, new Size(3, 3)), Size.Subtract(Size, new Size(6, 6))));

            int fontSize = 48;
            Font drawFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            graphics.DrawString(DrawnNumber == 0 ? "" : DrawnNumber.ToString(), drawFont, Brushes.Blue, Position.X, Position.Y);

            if (Selected)
            {
                Pen pen = new Pen(Color.Gray, 1);
                pen.Alignment = PenAlignment.Inset;
                graphics.DrawRectangle(pen, new Rectangle(Position, Size));
            }
        }
    }
}

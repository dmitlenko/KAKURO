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
        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TileType p = (TileType)obj;
                return Type == p.Type;
            }
        }
    }

    internal class GraphicTile
    {
        //public PictureBox canvas { get; set; }
        public Bitmap buffer { get; set; }
        public TileType Type { get; }

        public Point Position;
        public Size Size;
        public Point SizePoint { get => new Point(Size.Width, Size.Height); }

        public bool Selected { get; set; }

        public GraphicTile() { Type = TileTypes.Empty; Selected = false; Position = Point.Empty; Size = Size.Empty; }
        public GraphicTile(TileType type) { Type = type; Selected = false; Position = Point.Empty; Size = Size.Empty; }
        public GraphicTile(PictureBox pb, TileType type) { Type = type; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        public GraphicTile(PictureBox pb) { Type = TileTypes.Empty; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        public virtual void Draw(Graphics graphics) { }

        public void DrawSelection(Graphics graphics, Color color) {
            Pen pen = new Pen(color, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Position, Size));
        }

        public void DrawOutline(Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Position, Size));
        }
    }

    class BlackGraphicTile : GraphicTile
    {
        public BlackGraphicTile(PictureBox pictureBox) : base(pictureBox, TileTypes.Black) { }
        public BlackGraphicTile() : base(TileTypes.Black) { }

        public override void Draw(Graphics graphics) {
            if (Selected) DrawSelection(graphics, Color.Gray);
        }
    }

    class HintGraphicTile : GraphicTile
    {
        public int SumLeft { get; set; }
        public int SumRight { get; set; }

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

        public override void Draw(Graphics graphics)
        {
            //g.DrawRectangle(new Pen(Brushes.Black), new Rectangle(Position, Size));
            graphics.DrawLine(new Pen(Color.White, 2), Point.Add(Position, new Size(Size.Width/4, Size.Height/4)), Point.Add(Position, Size.Subtract(Size, new Size(1,1))));

            int fontSize = (Size.Height / 3) + 1;
            Font drawFont = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

            SizeF str1sz = graphics.MeasureString(SumLeft.ToString(), drawFont);
            SizeF str2sz = graphics.MeasureString(SumRight.ToString(), drawFont);

            graphics.DrawString(SumLeft == 0 ? "" : SumLeft.ToString(), drawFont, Brushes.White, Position.X, Position.Y + Size.Height - str1sz.Height);
            graphics.DrawString(SumRight == 0 ? "" : SumRight.ToString(), drawFont, Brushes.White, Position.X + Size.Width - str2sz.Width, Position.Y);

            DrawOutline(graphics);
            if (Selected) DrawSelection(graphics, Color.Gray);
        }
    }

    class NumberGraphicTile : GraphicTile
    {
        public int DrawnNumber { get; set; }

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
            graphics.FillRectangle(Brushes.White, new Rectangle(Position,Size));

            int fontSize = (int)(Size.Height * 0.8) + 1;
            Font drawFont = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            SizeF textSize = graphics.MeasureString(DrawnNumber.ToString(), drawFont);
            Size textSize1 = new Size(Size.Width / 2 - (int)textSize.Width / 2, Size.Height / 2 - (int)textSize.Height / 2);

            graphics.DrawString(DrawnNumber == 0 ? "" : DrawnNumber.ToString(), drawFont, Brushes.SteelBlue, Point.Add(Position,textSize1));

            DrawOutline(graphics);
            if (Selected) DrawSelection(graphics, Color.Gray);
        }
    }
}

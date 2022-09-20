using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro.Renderer
{
    public struct TileTypes
    {
        public static string Empty { get => "empty"; }
        public static string Black { get => "black"; }
        public static string Hint { get => "hint"; }
        public static string Number { get => "number"; }
    }

    public class GraphicTile
    {
        public string Type { get; }

        public Point Position;
        public Size Size;
        public Point SizePoint { get => new Point(Size.Width, Size.Height); }

        public bool Selected { get; set; }

        public GraphicTile() { Type = TileTypes.Empty; Selected = false; Position = Point.Empty; Size = Size.Empty; }
        public GraphicTile(string type) { Type = type; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        public virtual void Draw(Graphics graphics) { }

        public void DrawSelection(Graphics graphics)
        {
            int padding = 1;

            Pen pen = new Pen(Color.DodgerBlue, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Point.Add(Position, new Size(padding, padding)), Size.Subtract(Size, new Size(padding, padding))));
        }

        public void DrawOutline(Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Position, Size));
        }
    }

    public class BlackGraphicTile : GraphicTile
    {
        public BlackGraphicTile() : base(TileTypes.Black) { }

        public override void Draw(Graphics graphics)
        {
            if (Selected) DrawSelection(graphics);
        }
    }

    public class HintGraphicTile : GraphicTile
    {
        public int SumVertical { get; set; }
        public int SumHorizontal { get; set; }

        public bool HighlightVertical = false;
        public bool HighlightHorizontal = false;

        public bool HighlightVerticalSum = false;
        public bool HighlightHorizontalSum = false;

        public bool GrayVerticalSum = false;
        public bool GrayHorizontalSum = false;

        public HintGraphicTile() : base(TileTypes.Hint)
        {
            SumVertical = 0;
            SumHorizontal = 0;
        }

        public HintGraphicTile(int sumVertical, int sumHorizontal) : base(TileTypes.Hint)
        {
            SumVertical = sumVertical;
            SumHorizontal = sumHorizontal;
        }

        public override void Draw(Graphics graphics)
        {
            if (HighlightVertical || HighlightHorizontal)
            {
                Point[] points = { };
                Brush fillBrush = new SolidBrush(Color.FromArgb(0x25, 0x25, 0x25));

                if (HighlightHorizontal) points = new Point[] { Position, Point.Add(Position, new Size(Size.Width, 0)), Point.Add(Position, Size) };
                if (HighlightVertical) points = new Point[] { Position, Point.Add(Position, new Size(0, Size.Height)), Point.Add(Position, Size) };

                graphics.FillPolygon(fillBrush, points);
            }

            graphics.DrawLine(new Pen(Color.White, 2), Point.Add(Position, new Size(Size.Width / 4, Size.Height / 4)), Point.Add(Position, Size.Subtract(Size, new Size(1, 1))));

            int fontSize = Size.Height / 3 + 1;

            FontStyle verticalFontStyle = HighlightVerticalSum ? FontStyle.Bold : FontStyle.Regular;
            FontStyle horizontalFontStyle = HighlightHorizontalSum ? FontStyle.Bold : FontStyle.Regular;

            Font verticalFont = new Font(FontFamily.GenericSansSerif, fontSize, verticalFontStyle, GraphicsUnit.Pixel);
            Font horizontalFont = new Font(FontFamily.GenericSansSerif, fontSize, horizontalFontStyle, GraphicsUnit.Pixel);

            SizeF str1sz = graphics.MeasureString(SumVertical.ToString(), verticalFont);
            SizeF str2sz = graphics.MeasureString(SumHorizontal.ToString(), horizontalFont);

            Brush verticalColor = HighlightVerticalSum ? Brushes.DodgerBlue : GrayVerticalSum ? Brushes.Gray : Brushes.White;
            Brush horizontalColor = HighlightHorizontalSum ? Brushes.DodgerBlue : GrayHorizontalSum ? Brushes.Gray : Brushes.White;

            graphics.DrawString(SumVertical == 0 ? "" : SumVertical.ToString(), verticalFont, verticalColor, Position.X, Position.Y + Size.Height - str1sz.Height);
            graphics.DrawString(SumHorizontal == 0 ? "" : SumHorizontal.ToString(), horizontalFont, horizontalColor, Position.X + Size.Width - str2sz.Width, Position.Y);

            DrawOutline(graphics);
            if (Selected) DrawSelection(graphics);
        }
    }

    public class NumberGraphicTile : GraphicTile
    {
        public int DrawnNumber { get; set; }
        public bool Highlight { get; set; }

        public NumberGraphicTile() : base(TileTypes.Number) { DrawnNumber = 0; }

        public NumberGraphicTile(int drawnNumber) : base(TileTypes.Number)
        {
            DrawnNumber = drawnNumber;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, new Rectangle(Position, Size));

            int fontSize = (int)(Size.Height * 0.8) + 1;
            Font drawFont = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);

            SizeF textSize = graphics.MeasureString(DrawnNumber.ToString(), drawFont);
            Size textSize1 = new Size(Size.Width / 2 - (int)textSize.Width / 2, Size.Height / 2 - (int)textSize.Height / 2);

            graphics.DrawString(DrawnNumber == 0 ? "" : DrawnNumber.ToString(), drawFont, Highlight ? Brushes.Blue : Brushes.DodgerBlue, Point.Add(Position, textSize1));

            DrawOutline(graphics);
            if (Selected) DrawSelection(graphics);
        }
    }
}

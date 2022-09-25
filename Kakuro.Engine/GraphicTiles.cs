using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro.Engine
{
    /**
     * <summary>Struct that represents types of graphic tiles</summary>
     */
    public struct TileTypes
    {
        public static string Empty { get => "empty"; }
        public static string Black { get => "black"; }
        public static string Hint { get => "hint"; }
        public static string Number { get => "number"; }
    }

    /**
     * <summary>Base class for GraphicTile</summary>
     */
    public class GraphicTile
    {
        /**
         * <summary>Type of the tile</summary>
         */
        public string Type { get; }

        /**
         * <summary>Position of the tile on the screen</summary>
         */
        public Point Position;

        /**
         * <summary>Size of the tile in pixels</summary>
         */
        public Size Size;

        /**
         * <summary>Size of the tile in pixels</summary>
         * <value>Point(Size.Width, Size.Height)</value>
         */
        public Point SizePoint { get => new Point(Size.Width, Size.Height); }

        /**
         * <summary>Is tile selected</summary>
         */
        public bool Selected { get; set; }

        /**
         * <summary>Base constructor for GraphicTile</summary>
         */
        public GraphicTile() { Type = TileTypes.Empty; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        /**
         * <summary>Constructor for GraphicTile</summary>
         * <param name="type">Type of the tile</param>
         */
        public GraphicTile(string type) { Type = type; Selected = false; Position = Point.Empty; Size = Size.Empty; }

        /**
         * <summary>Virtual method that draws tile on the canvas</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
        public virtual void Draw(Graphics graphics) { }

        /**
         * <summary>Method that draws blue outline if the tile is selected</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
        public void DrawSelection(Graphics graphics)
        {
            int padding = 1;

            Pen pen = new Pen(Color.DodgerBlue, 2);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Point.Add(Position, new Size(padding, padding)), Size.Subtract(Size, new Size(padding, padding))));
        }

        /**
         * <summary>Method that draws black outline</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
        public void DrawOutline(Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Position, Size));
        }
    }

    /**
     * <summary>Class that represents black graphic tile</summary>
     */
    public class BlackGraphicTile : GraphicTile
    {
        /**
         * <summary>Constructor for BlackGraphicTile class</summary>
         */
        public BlackGraphicTile() : base(TileTypes.Black) { }

        /**
         * <summary>Virtual method that draws BlackGraphicTile on the canvas</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
        public override void Draw(Graphics graphics)
        {
            if (Selected) DrawSelection(graphics);
        }
    }

    /**
     * <summary>Class that represents sum graphic tile</summary>
     */
    public class SumGraphicTile : GraphicTile
    {
        /**
         * <summary>Variable that represents vertical sum of the SumGraphicTile</summary>
         */
        public int SumVertical { get; set; }

        /**
         * <summary>Variable that represents horizontal sum of the SumGraphicTile</summary>
         */
        public int SumHorizontal { get; set; }

        /**
         * <summary>If <c>true</c>, then highlight vertical sum polygon</summary>
         */
        public bool HighlightVertical { get; set; } = false;

        /**
         * <summary>If <c>true</c>, then highlight horizontal sum polygon</summary>
         */
        public bool HighlightHorizontal { get; set; } = false;

        /**
         * <summary>If <c>true</c>, then highlight vertical sum text</summary>
         */
        public bool HighlightVerticalSum { get; set; } = false;

        /**
         * <summary>If <c>true</c>, then highlight horizontal sum text</summary>
         */
        public bool HighlightHorizontalSum { get; set; } = false;

        /**
         * <summary>If <c>true</c>, then gray-out vertical sum text</summary>
         */
        public bool GrayVerticalSum { get; set; } = false;

        /**
         * <summary>If <c>true</c>, then gray-out horizontal sum text</summary>
         */
        public bool GrayHorizontalSum { get; set; } = false;

        /**
         * <summary>Default constructor for SumGraphicTiles</summary>
         */
        public SumGraphicTile() : base(TileTypes.Hint)
        {
            SumVertical = 0;
            SumHorizontal = 0;
        }

        /**
         * <summary>Constructor that creates instance of SumGraphicTile with <paramref name="sumVertical"/> and <paramref name="sumHorizontal"/></summary>
         * <param name="sumVertical">Vertical sum</param>
         * <param name="sumHorizontal">Horizontal sum</param>
         */
        public SumGraphicTile(int sumVertical, int sumHorizontal) : base(TileTypes.Hint)
        {
            SumVertical = sumVertical;
            SumHorizontal = sumHorizontal;
        }

        /**
         * <summary>Method that draws SumGraphicTile on the canvas</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
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

    /**
     * <summary>Class that represents white graphic tile</summary>
     */
    public class WhiteGraphicTile : GraphicTile
    {
        /**
         * <summary>Variable that represents number drawn on tile</summary>
         */
        public int DrawnNumber { get; set; }

        /**
         * <summary>If <c>true</c>, the highlight drawn number</summary>
         */
        public bool Highlight { get; set; }

        /**
         * <summary>Default constructor for WhiteGraphicTile</summary>
         */
        public WhiteGraphicTile() : base(TileTypes.Number) { DrawnNumber = 0; }

        /**
         * <summary>Constructor that creates instance of WhiteGraphicTile with <paramref name="drawnNumber"/></summary>
         * <param name="drawnNumber">Number that gonna be drawn on tile</param>
         */
        public WhiteGraphicTile(int drawnNumber) : base(TileTypes.Number)
        {
            DrawnNumber = drawnNumber;
        }

        /**
         * <summary>Method that draws WhiteGraphicTile on the canvas</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
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

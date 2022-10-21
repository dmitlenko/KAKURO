using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Graphics
{
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
        public override void Draw(System.Drawing.Graphics graphics)
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

            graphics.DrawString(SumVertical == -1 ? "" : SumVertical.ToString(), verticalFont, verticalColor, Position.X, Position.Y + Size.Height - str1sz.Height);
            graphics.DrawString(SumHorizontal == -1 ? "" : SumHorizontal.ToString(), horizontalFont, horizontalColor, Position.X + Size.Width - str2sz.Width, Position.Y);

            DrawOutline(graphics);
            if (Selected) DrawSelection(graphics);
        }
    }
}

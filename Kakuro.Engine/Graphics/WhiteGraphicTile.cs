using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Graphics
{
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

        /// <summary>
        /// Background color of the tile
        /// </summary>
        public bool HighlightBackground { get; set; }

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
        public override void Draw(System.Drawing.Graphics graphics)
        {
            graphics.FillRectangle(HighlightBackground ? new SolidBrush(Color.FromArgb(255, 230, 255, 230)) : Brushes.White, new Rectangle(Position, Size));

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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro.Engine.Graphics
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

        /// <summary>
        /// Use blue for errors
        /// </summary>
        public bool BlueForErrors { get; set; } = false;

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
        public virtual void Draw(System.Drawing.Graphics graphics) { }

        /**
         * <summary>Method that draws blue outline if the tile is selected</summary>
         * <param name="graphics">Buffer for the graphics</param>
         */
        public void DrawSelection(System.Drawing.Graphics graphics)
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
        public void DrawOutline(System.Drawing.Graphics graphics)
        {
            Pen pen = new Pen(Color.Black, 1);
            pen.Alignment = PenAlignment.Inset;
            graphics.DrawRectangle(pen, new Rectangle(Position, Size));
        }
    }
}

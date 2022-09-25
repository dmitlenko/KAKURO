using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Engine.Graphics
{
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
        public override void Draw(System.Drawing.Graphics graphics)
        {
            if (Selected) DrawSelection(graphics);
        }
    }
}

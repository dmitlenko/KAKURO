using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    public struct TileTypes
    {
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
        public PictureBox Picture { get; set; }
        public Bitmap buffer { get; set; }
        public TileType Type { get; }

        public GraphicTile(TileType type) { Picture = null; Type = type; }
        public GraphicTile(PictureBox pb, TileType type) { Picture = pb; Type = type; }
        
        public GraphicTile(PictureBox pb) { Picture = pb; Type = null; }

        public virtual void Draw() { }
    }

    class BlackGraphicTile : GraphicTile
    {
        public BlackGraphicTile(PictureBox pictureBox) : base(pictureBox, TileTypes.Black) { }
        public BlackGraphicTile() : base(TileTypes.Black) { }

        public override void Draw() {
            buffer = new Bitmap(Picture.Width, Picture.Height);

            Task.Factory.StartNew(() =>
            {
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    g.Clear(Color.Black);
                }

                Picture.Invoke(new Action(() =>
                {
                    Picture.Image = buffer;
                }));
            });
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

        public override void Draw()
        {
            buffer = new Bitmap(Picture.Width, (Picture.Height));

            Task.Factory.StartNew(() =>
            {
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    g.Clear(Color.Black);
                    g.DrawLine(new Pen(Color.White, 2), new Point(-2, -2), new Point(Picture.Width + 2, Picture.Height + 2));

                    int padding = 2;
                    int fontSize = 16;
                    Font drawFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                    g.DrawString(SumLeft == 0 ? "" : SumLeft.ToString(), drawFont, Brushes.White, padding, Picture.Height - GetStringWidth(SumRight.ToString(), drawFont).Item2);
                    g.DrawString(SumRight == 0 ? "" : SumRight.ToString(), drawFont, Brushes.White, Picture.Width - GetStringWidth(SumRight.ToString(), drawFont).Item1, padding);
                }

                Picture.Invoke(new Action(() =>
                {
                    Picture.Image = buffer;
                }));
            });
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

        public override void Draw()
        {
            buffer = new Bitmap(Picture.Width, Picture.Height);

            Task.Factory.StartNew(() =>
            {
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    g.Clear(Color.Black);
                    g.FillRectangle(Brushes.White, new Rectangle(3, 3, Picture.Width - 3, Picture.Height - 3));

                    int fontSize = 48;
                    Font drawFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                    g.DrawString(DrawnNumber == 0 ? "" : DrawnNumber.ToString(), drawFont, Brushes.Blue, 0, 0);
                }

                Picture.Invoke(new Action(() =>
                {
                    Picture.Image = buffer;
                }));
            });
        }
    }
}

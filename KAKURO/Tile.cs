using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    internal class Tile
    {
        public PictureBox Picture { get; set; }

        public Tile(PictureBox pb)
        {
            Picture = pb;
        }

        public virtual void Draw() { }
    }

    class BlackTile : Tile
    {
        public BlackTile(PictureBox pictureBox) : base(pictureBox) { }

        private void Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Picture.Width, Picture.Height));
        }

        public override void Draw() {
            Picture.Paint += new PaintEventHandler(Paint);
        }
    }

    class SumsTile : Tile
    {
        public int SumLeft {get;set;}
        public int SumRight {get;set;}

        public SumsTile(PictureBox pictureBox, int sumLeft, int sumRight): base(pictureBox) 
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

        private void Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Picture.Width, Picture.Height));
            e.Graphics.DrawLine(new Pen(Color.White, 2), new Point(-2, -2), new Point(Picture.Width + 2, Picture.Height + 2));

            int padding = 2;
            int fontSize = 16;
            Font drawFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            e.Graphics.DrawString(SumLeft == 0 ? "" : SumLeft.ToString(), drawFont , Brushes.White, padding, Picture.Height - GetStringWidth(SumRight.ToString(), drawFont).Item2);
            e.Graphics.DrawString(SumRight == 0 ? "" : SumRight.ToString(), drawFont , Brushes.White, Picture.Width - GetStringWidth(SumRight.ToString(), drawFont).Item1, padding);

        }

        public override void Draw()
        {
            Picture.Paint += new PaintEventHandler(Paint);
        }
    }

    class NumberTile : Tile
    {
        private int DrawnNumber { get; set; }

        public NumberTile(PictureBox pictureBox, int drawnNumber) : base(pictureBox)
        {
            DrawnNumber = drawnNumber;
        }

        private void Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(3, 3, Picture.Width-3, Picture.Height-3));

            int fontSize = 48;
            Font drawFont = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

            e.Graphics.DrawString(DrawnNumber == 0 ? "" : DrawnNumber.ToString(), drawFont, Brushes.Blue, 0, 0);
        }

        public override void Draw()
        {
            Picture.Paint += new PaintEventHandler(Paint);
        }
    }
}

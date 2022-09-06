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

        private void Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Picture.Width, Picture.Height));
            e.Graphics.DrawLine(new Pen(Color.White,2), new Point(10, 10), new Point(Picture.Width-5, Picture.Height-5));

            Font drawFont = new Font(new FontFamily("monospace"), 8, FontStyle.Regular, GraphicsUnit.Point);

            e.Graphics.DrawString(SumLeft.ToString(), drawFont , Brushes.White, 0, Picture.Height - 10);
            e.Graphics.DrawString(SumRight.ToString(), drawFont , Brushes.White, 0, Picture.Height - 10);

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
            e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(1, 1, Picture.Width-1, Picture.Height-1));
        }
    }
}

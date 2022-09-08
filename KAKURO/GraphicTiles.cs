using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    internal class GraphicTile
    {
        public PictureBox Picture { get; set; }

        public GraphicTile() { Picture = null; }
        public GraphicTile(PictureBox pb)
        {
            Picture = pb;
        }

        public virtual void Draw() { }
    }

    class BlackGraphicTile : GraphicTile
    {
        public BlackGraphicTile(PictureBox pictureBox) : base(pictureBox) { }
        public BlackGraphicTile() : base() { }

        private void Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Picture.Width, Picture.Height));
        }

        public override void Draw() {
            if(Picture != null)
                Picture.Paint += new PaintEventHandler(Paint);
        }
    }

    class HintGraphicTile : GraphicTile
    {
        public int SumLeft {get;set;}
        public int SumRight {get;set;}

        public HintGraphicTile() : base()
        {
            SumLeft = 0;
            SumRight = 0;
        }

        public HintGraphicTile(int sumLeft, int sumRight) : base()
        {
            SumLeft = sumLeft;
            SumRight = sumRight;
        }

        public HintGraphicTile(PictureBox pictureBox, int sumLeft, int sumRight): base(pictureBox) 
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
            if(Picture != null)
                Picture.Paint += new PaintEventHandler(Paint);
        }
    }

    class NumberGraphicTile : GraphicTile
    {
        private int DrawnNumber { get; set; }

        public NumberGraphicTile(): base() { DrawnNumber = 0; }

        public NumberGraphicTile(int drawnNumber) : base()
        {
            DrawnNumber = drawnNumber;
        }

        public NumberGraphicTile(PictureBox pictureBox, int drawnNumber) : base(pictureBox)
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
            if(Picture == null)
                Picture.Paint += new PaintEventHandler(Paint);
        }
    }
}

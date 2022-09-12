using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    internal class TileController
    {
        private Point PrevSelected = new Point(0, 0);

        public PictureBox Canvas { get; }
        public GraphicTile[,] GraphicTiles;

        public Point Selected = new Point(0,0);
        public Size Size { get; set; }
        public int SizeX { get => GraphicTiles.GetLength(1); }
        public int SizeY { get => GraphicTiles.GetLength(0); }

        public bool HighlightSums { get; set; }

        public struct Box
        {
            private GraphicTile tile;
            public Box(ref GraphicTile b) => tile = b;

            public void Move(int x, int y)
            {
                tile.Position = new Point(x, y);
            }

            public void Resize(int width, int height)
            {
                tile.Size = new Size(width, height);
            }
        }

        private bool _enabled = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled = value) EnableCanvas();
                else DisableCanvas();
            }
        }

        public TileController(PictureBox canvas, int tilesX, int tilesY, GraphicTile[,] tiles)
        {
            GraphicTiles = new GraphicTile[tilesY, tilesX];
            Canvas = canvas;
            Size = new Size(tilesX, tilesY);

            PrepareBoxTiles();

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    GraphicTiles[i, j] = tiles[i, j];

            PrepareCanvas();
            Update();
        }

        // Canvas click
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            Point sel = TileCoordsByPoint(new Point(e.X, e.Y));
            if (sel.X > -1 && sel.X < Size.Width && sel.Y > -1 && sel.Y < Size.Height)
            {
                Selected = sel;

                Update();
            }
        }

        private void PrepareBoxTiles()
        {
            int tileHW = Canvas.Height / Size.Height;
            for (int i = 0; i < GraphicTiles.GetLength(0); i++)
                for(int j = 0; j < GraphicTiles.GetLength(1); j++)
                {
                    GraphicTiles[i, j] = new BlackGraphicTile(Canvas);
                    GraphicTiles[i, j].Position = new Point(i * tileHW, j * tileHW);
                    GraphicTiles[i, j].Size = new Size(tileHW, tileHW);
                }
        }

        private void PrepareCanvas()
        {
            Canvas.BackColor = Color.Black;
            Canvas.MouseUp += new MouseEventHandler(Canvas_MouseUp);
            Canvas.Image = new Bitmap(Canvas.Width, Canvas.Height);
        }

        private Point TileCoordsByPoint(Point p)
        {
            float tileHW = Canvas.Height / Size.Height;

            return new Point((int) Math.Floor(p.X / tileHW), (int) Math.Floor(p.Y / tileHW));
        }

        private void DisableCanvas() => Canvas.Enabled = false;
        private void EnableCanvas() => Canvas.Enabled = true;

        private Point TopHintFromSelection()
        {
            for (int i = Selected.Y; i >= 0; i--)
                if (GraphicTiles[i, Selected.X].Type.ToString() == "hint")
                    return new Point(Selected.X, i);

            return Point.Empty;
        }

        private Point LeftHintFromSelection()
        {
            for (int i = Selected.X; i >= 0; i--)
                if (GraphicTiles[Selected.Y, i].Type.ToString() == "hint")
                    return new Point(i, Selected.Y);

            return Point.Empty;
        }

        public Box this[int x, int y] { get => new Box(ref GraphicTiles[y, x]); }

        public void AssignTiles(GraphicTile[,] tiles) =>  GraphicTiles = tiles;

        public void MoveSelectionUp()
        {
            if (Enabled)
            {
                if (Selected.Y > 0) Selected.Y -= 1;

                Update();
            }
        }

        public void MoveSelectionDown()
        {
            if (Enabled)
            {
                if (Selected.Y < GraphicTiles.GetLength(0) - 1) Selected.Y += 1;

                Update();
            }
        }

        public void MoveSelectionLeft()
        {
            if (Enabled)
            {
                if (Selected.X > 0) Selected.X -= 1;

                Update();
            }
        }

        public void MoveSelectionRight()
        {
            if (Enabled)
            {
                if (Selected.X < GraphicTiles.GetLength(1) - 1) Selected.X += 1;

                Update();
            }
        }

        public void SetTileNumber(int number)
        {
            if (Enabled)
            {
                if (GraphicTiles[Selected.Y, Selected.X].Type.ToString() == "number")
                    ((NumberGraphicTile)GraphicTiles[Selected.Y, Selected.X]).DrawnNumber = number;

                Update();
            }
        }

        public void Update()
        {
            Bitmap buffer = new Bitmap(Canvas.Width + 1, Canvas.Height + 1);

            Task.Factory.StartNew(() =>
            {
                int tileHW = Canvas.Width / Size.Width;
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    for (int i = 0; i < GraphicTiles.GetLength(0); i++)
                    {
                        for (int j = 0; j < GraphicTiles.GetLength(1); j++)
                        {
                            GraphicTiles[i, j].Size = new Size(tileHW, tileHW);
                            GraphicTiles[i, j].Position = new Point(j * tileHW, i * tileHW);
                            GraphicTiles[i, j].Selected = Selected.X == j && Selected.Y == i;

                            if (GraphicTiles[i, j].Type.ToString() == "hint" && GraphicTiles[i, j].Type.ToString() == "hint")
                            {
                                ((HintGraphicTile)GraphicTiles[i, j]).HighlightVertical = false;
                                ((HintGraphicTile)GraphicTiles[i, j]).HighlightHorizontal = false;
                            }

                            GraphicTiles[i, j].Draw(g);
                        }
                    }

                    if (HighlightSums)
                    {
                        Point th = TopHintFromSelection();
                        Point lh = LeftHintFromSelection();

                        if(GraphicTiles[th.Y, th.X].Type.ToString() == "hint" &&  GraphicTiles[lh.Y, lh.X].Type.ToString() == "hint" && Selected != th && Selected != lh)
                        {
                            ((HintGraphicTile)GraphicTiles[th.Y, th.X]).HighlightVertical = true;
                            ((HintGraphicTile)GraphicTiles[lh.Y, lh.X]).HighlightHorizontal = true;

                            GraphicTiles[th.Y, th.X].Draw(g);
                            GraphicTiles[lh.Y, lh.X].Draw(g);
                        }
                    }
                }

                Canvas.Invoke(new Action(() => {
                    Canvas.Image = buffer;
                }));
            });

        }
    }
}

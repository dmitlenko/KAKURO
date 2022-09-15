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
        private bool _enabled = true;

        public PictureBox Canvas { get; set; }
        public GraphicTile[,] GraphicTiles;

        public Point Selected = new Point(0,0);
        public Size Size { get; set; }
        public int SizeX { get => GraphicTiles.GetLength(1); }
        public int SizeY { get => GraphicTiles.GetLength(0); }

        public bool HighlightSelectionSums { get; set; }
        public bool HighlightWrongSums { get; set; }
        public bool HighlightDuplicates { get; set; }
        public bool GrayCompleteSums { get; set; } 

        public bool Enabled 
        { 
            get => _enabled; 
            set
            {
                _enabled = value;
                Update();
            } 
        }

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

        public TileController(PictureBox canvas) 
        {
            Canvas = canvas;
            Enabled = false;

            PrepareCanvas();
        }

        public TileController(PictureBox canvas, int tilesX, int tilesY) 
        {
            Canvas = canvas;
            Size = new Size(tilesX, tilesY);
            Enabled = false;

            PrepareCanvas();
        }

        public TileController(PictureBox canvas, int tilesX, int tilesY, Cell[,] cells)
        {
            Canvas = canvas;
            Size = new Size(tilesX, tilesY);
            Enabled = true;

            AssignCells(cells);

            PrepareCanvas();
            Update();
        }

        public void LoadSettings()
        {
            HighlightDuplicates = Properties.Settings.Default.HighlightDuplicates;
            HighlightSelectionSums = Properties.Settings.Default.HighlightSelectionSums;
            HighlightWrongSums = Properties.Settings.Default.HighlightWrongSums;
            GrayCompleteSums = Properties.Settings.Default.GrayCompleteSums;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (Enabled)
            {
                Point sel = TileCoordsByPoint(new Point(e.X, e.Y));
                if (sel.X > -1 && sel.X < Size.Width && sel.Y > -1 && sel.Y < Size.Height)
                {
                    Selected = sel;

                    Update();
                }
            }
        }

        private void PrepareCanvas()
        {
            Canvas.BackColor = Color.Black;
            Canvas.MouseDown += new MouseEventHandler(Canvas_MouseDown);
            Canvas.Image = new Bitmap(Canvas.Width, Canvas.Height);
        }

        private Point TileCoordsByPoint(Point p)
        {
            float tileHW = Canvas.Height / Size.Height;

            return new Point((int) Math.Floor(p.X / tileHW), (int) Math.Floor(p.Y / tileHW));
        }

        private Point TopHintFromSelection()
        {
            for (int i = Selected.Y; i >= 0; i--)
                if (GraphicTiles[i, Selected.X].Type == TileTypes.Hint)
                    return new Point(Selected.X, i);

            return Point.Empty;
        }

        private Point LeftHintFromSelection()
        {
            for (int i = Selected.X; i >= 0; i--)
                if (GraphicTiles[Selected.Y, i].Type == TileTypes.Hint)
                    return new Point(i, Selected.Y);

            return Point.Empty;
        }

        private int HorizontalSum(int x, int y)
        {
            int sum = 0;
            int num;

            for (int i = x + 1; i < GraphicTiles.GetLength(1); i++)
            {
                if (GraphicTiles[y, i].Type == TileTypes.Black || GraphicTiles[y, i].Type == TileTypes.Hint) break;

                if (GraphicTiles[y, i].Type == TileTypes.Number)
                {
                    num = ((NumberGraphicTile)GraphicTiles[y, i]).DrawnNumber;
                    sum += num;
                }
            }

            return sum;
        }

        private int VerticalSum(int x, int y)
        {
            int sum = 0;
            int num;

            for (int i = y + 1; i < GraphicTiles.GetLength(0); i++)
            {
                if (GraphicTiles[i, x].Type == TileTypes.Black || GraphicTiles[i, x].Type == TileTypes.Hint) break;

                if (GraphicTiles[i, x].Type == TileTypes.Number)
                {
                    num = ((NumberGraphicTile)GraphicTiles[i, x]).DrawnNumber;
                    sum += num;
                }
            }

            return sum;
        }

        public Box this[int x, int y] { get => new Box(ref GraphicTiles[y, x]); }

        public void AssignCells(Cell[,] cells)
        {
            GraphicTile[,] tiles = new GraphicTile[Size.Height, Size.Width];

            for (int i = 0; i < Size.Height; i++)
                for (int j = 0; j < Size.Height; j++)
                    tiles[i, j] = new BlackGraphicTile();

            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                    if (cells[i, j].Type == "black" || cells[i, j].Type == "cell")
                        tiles[i, j] = new BlackGraphicTile();
                    else if (cells[i, j].Type == "hint")
                        tiles[i, j] = new HintGraphicTile(((HintCell)cells[i, j]).VerticalSum, ((HintCell)cells[i, j]).HorizontalSum);
                    else
                        tiles[i, j] = new NumberGraphicTile(((NumberCell)cells[i, j]).Number);

            GraphicTiles = tiles;
        }

        public Cell[,] CellData()
        {
            Cell[,] cells = new Cell[Size.Height, Size.Width];

            for (int i = 0; i < Size.Height; i++)
                for (int j = 0; j < Size.Width; j++)
                    if (GraphicTiles[i, j].Type == TileTypes.Black || GraphicTiles[i, j].Type == TileTypes.Empty)
                        cells[i, j] = new Cell();
                    else if (GraphicTiles[i, j].Type == TileTypes.Hint)
                        cells[i, j] = new HintCell(((HintGraphicTile)GraphicTiles[i, j]).SumVertical, ((HintGraphicTile)GraphicTiles[i, j]).SumHorizontal);
                    else
                        cells[i, j] = new NumberCell(((NumberGraphicTile)GraphicTiles[i, j]).DrawnNumber);

            return cells;
        }

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
                if (GraphicTiles[Selected.Y, Selected.X].Type == TileTypes.Number)
                    ((NumberGraphicTile)GraphicTiles[Selected.Y, Selected.X]).DrawnNumber = number;

                Update();
            }
        }

        public void Update()
        {
            if (Size.Width <= 0 && Size.Height <= 0) return;

            Bitmap buffer = new Bitmap(Canvas.Width + 1, Canvas.Height + 1);

            Task.Factory.StartNew(() =>
            {
                int tileHW = Canvas.Width / Size.Width;
                int sumH, sumV, sumH1, sumV1;

                using (Graphics g = Graphics.FromImage(buffer))
                {
                    if (GraphicTiles == null) return;

                    for (int i = 0; i < GraphicTiles.GetLength(0); i++)
                    {
                        for (int j = 0; j < GraphicTiles.GetLength(1); j++)
                        {
                            GraphicTiles[i, j].Size = new Size(tileHW, tileHW);
                            GraphicTiles[i, j].Position = new Point(j * tileHW, i * tileHW);
                            GraphicTiles[i, j].Selected = Selected.X == j && Selected.Y == i;

                            if (GraphicTiles[i, j].Type == TileTypes.Hint)
                            {
                                ((HintGraphicTile)GraphicTiles[i, j]).HighlightVertical = false;
                                ((HintGraphicTile)GraphicTiles[i, j]).HighlightHorizontal = false;

                                if (HighlightWrongSums)
                                {
                                    sumH = ((HintGraphicTile)GraphicTiles[i, j]).SumHorizontal;
                                    sumV = ((HintGraphicTile)GraphicTiles[i, j]).SumVertical;

                                    sumH1 = HorizontalSum(j, i);
                                    sumV1 = VerticalSum(j, i);

                                    ((HintGraphicTile)GraphicTiles[i, j]).HighlightHorizontalSum = sumH < sumH1;
                                    ((HintGraphicTile)GraphicTiles[i, j]).HighlightVerticalSum = sumV < sumV1;
                                }

                                if (GrayCompleteSums)
                                {
                                    sumH = ((HintGraphicTile)GraphicTiles[i, j]).SumHorizontal;
                                    sumV = ((HintGraphicTile)GraphicTiles[i, j]).SumVertical;

                                    sumH1 = HorizontalSum(j, i);
                                    sumV1 = VerticalSum(j, i);

                                    ((HintGraphicTile)GraphicTiles[i, j]).GrayHorizontalSum = sumH == sumH1;
                                    ((HintGraphicTile)GraphicTiles[i, j]).GrayVerticalSum = sumV == sumV1;
                                }
                            }

                            GraphicTiles[i, j].Draw(g);
                        }
                    }

                    if (HighlightSelectionSums)
                    {
                        Point th = TopHintFromSelection();
                        Point lh = LeftHintFromSelection();

                        if (GraphicTiles[th.Y, th.X].Type == TileTypes.Hint && GraphicTiles[lh.Y, lh.X].Type == TileTypes.Hint && Selected != th && Selected != lh)
                        {
                            ((HintGraphicTile)GraphicTiles[th.Y, th.X]).HighlightVertical = true;
                            ((HintGraphicTile)GraphicTiles[lh.Y, lh.X]).HighlightHorizontal = true;

                            GraphicTiles[th.Y, th.X].Draw(g);
                            GraphicTiles[lh.Y, lh.X].Draw(g);
                        }
                    }

                    if (!Enabled)
                        g.FillRectangle(new SolidBrush(Color.FromArgb(0xd0,Color.White)), new Rectangle(0, 0, Canvas.Width, Canvas.Height));
                }

                Canvas.Invoke(new Action(() => {
                    Canvas.Image = buffer;
                }));
            });

        }
    }
}

using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro.Engine.Graphics
{
    public class Renderer
    {
        private bool _enabled = true;
        private MouseEventHandler _clickHandler;

        public PictureBox Canvas { get; set; }
        public GraphicTile[,] GraphicTiles = { };

        public Point Selected = new Point(0, 0);
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

        public Renderer(PictureBox canvas)
        {
            Canvas = canvas;
            Enabled = false;

            PrepareCanvas();
        }

        public Renderer(PictureBox canvas, int tilesX, int tilesY)
        {
            Canvas = canvas;
            Size = new Size(tilesX, tilesY);
            Enabled = false;

            PrepareCanvas();
        }

        public Renderer(PictureBox canvas, int tilesX, int tilesY, KakuroBoard board)
        {
            Canvas = canvas;
            Size = new Size(tilesX, tilesY);
            Enabled = true;

            AssignBoard(board);

            PrepareCanvas();
            Update();
        }

        ~Renderer()
        {
            Canvas.MouseDown -= _clickHandler;
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
            _clickHandler = new MouseEventHandler(Canvas_MouseDown);
            Canvas.MouseDown += _clickHandler;
            Canvas.Image = new Bitmap(Canvas.Width, Canvas.Height);
        }

        private Point TileCoordsByPoint(Point p)
        {
            float tileHW = Canvas.Height / Size.Height;

            return new Point((int)Math.Floor(p.X / tileHW), (int)Math.Floor(p.Y / tileHW));
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

        private void OffsetForType(int x, int y, int xoff, int yoff, string type, Func<GraphicTile, bool> action)
        {
            x += xoff;
            y += yoff;
            if (type != GraphicTiles[y, x].Type || !action(GraphicTiles[y, x])) return;
            else OffsetForType(x, y, xoff, yoff, type, action);
        }

        private int OffsetNumberSum(int x, int y, int xoff, int yoff)
        {
            int sum = 0;

            OffsetForType(x, y, xoff, yoff, TileTypes.Number, (tile) =>
            {
                sum += (tile as WhiteGraphicTile).DrawnNumber;
                return true;
            });

            return sum;
        }

        private int HorizontalSum(int x, int y) => OffsetNumberSum(x, y, 1, 0);

        private int VerticalSum(int x, int y) => OffsetNumberSum(x, y, 0, 1);

        public GraphicTile this[int x, int y] => GraphicTiles[y, x];

        public void AssignBoard(KakuroBoard board)
        {
            Cell[,] cells = board.Grid;

            Size = new Size(cells.GetLength(1), cells.GetLength(0));
            GraphicTile[,] tiles = new GraphicTile[Size.Height, Size.Width];

            for (int i = 0; i < Size.Height; i++)
                for (int j = 0; j < Size.Height; j++)
                    tiles[i, j] = new BlackGraphicTile();

            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                    if (cells[i, j] is BlackCell)
                        tiles[i, j] = new BlackGraphicTile();
                    else if (cells[i, j] is SumCell)
                        tiles[i, j] = new SumGraphicTile((cells[i, j] as SumCell).ColSum, (cells[i, j] as SumCell).RowSum);
                    else
                        tiles[i, j] = new WhiteGraphicTile();

            Enabled = true; // якийсь баг робить щоб контроллер виключався
            GraphicTiles = tiles;

            Update();
        }

        /*public Cell[,] CellData()
        {
            Cell[,] cells = new Cell[Size.Height, Size.Width];

            for (int i = 0; i < Size.Height; i++)
                for (int j = 0; j < Size.Width; j++)
                    if (GraphicTiles[i, j].Type == TileTypes.Black || GraphicTiles[i, j].Type == TileTypes.Empty)
                        cells[i, j] = new BlackCell();
                    else if (GraphicTiles[i, j].Type == TileTypes.Hint)
                        cells[i, j] = new SumCell(((SumGraphicTile)GraphicTiles[i, j]).SumVertical, ((SumGraphicTile)GraphicTiles[i, j]).SumHorizontal);
                    else
                        cells[i, j] = new WhiteCell(((WhiteGraphicTile)GraphicTiles[i, j]).DrawnNumber);

            return cells;
        }*/

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

        public void SetSelectedTileNumber(int number)
        {
            if (Enabled)
            {
                if (GraphicTiles[Selected.Y, Selected.X].Type == TileTypes.Number)
                    ((WhiteGraphicTile)GraphicTiles[Selected.Y, Selected.X]).DrawnNumber = number;

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

                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(buffer))
                {
                    if (GraphicTiles == null) return;

                    for (int i = 0; i < GraphicTiles.GetLength(0); i++)
                    {
                        for (int j = 0; j < GraphicTiles.GetLength(1); j++)
                        {
                            if (GraphicTiles[i, j] == null) continue;

                            GraphicTiles[i, j].Size = new Size(tileHW, tileHW);
                            GraphicTiles[i, j].Position = new Point(j * tileHW, i * tileHW);
                            GraphicTiles[i, j].Selected = Selected.X == j && Selected.Y == i;

                            switch (GraphicTiles[i, j].Type)
                            {
                                case "hint":
                                    SumGraphicTile tile = GraphicTiles[i, j] as SumGraphicTile;

                                    tile.HighlightVertical = false;
                                    tile.HighlightHorizontal = false;

                                    if (HighlightWrongSums)
                                    {
                                        sumH = tile.SumHorizontal;
                                        sumV = tile.SumVertical;

                                        sumH1 = HorizontalSum(j, i);
                                        sumV1 = VerticalSum(j, i);

                                        tile.HighlightHorizontalSum = sumH < sumH1;
                                        tile.HighlightVerticalSum = sumV < sumV1;
                                    }

                                    if (GrayCompleteSums)
                                    {
                                        sumH = tile.SumHorizontal;
                                        sumV = tile.SumVertical;

                                        sumH1 = HorizontalSum(j, i);
                                        sumV1 = VerticalSum(j, i);

                                        tile.GrayHorizontalSum = sumH == sumH1;
                                        tile.GrayVerticalSum = sumV == sumV1;
                                    }

                                    break;

                                case "number":
                                    WhiteGraphicTile numberTile = GraphicTiles[i, j] as WhiteGraphicTile;

                                    // Set to default
                                    // numberTile.Highlight = false;

                                    if (HighlightDuplicates)
                                    {
                                        // todo
                                    }

                                    break;
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
                            ((SumGraphicTile)GraphicTiles[th.Y, th.X]).HighlightVertical = true;
                            ((SumGraphicTile)GraphicTiles[lh.Y, lh.X]).HighlightHorizontal = true;

                            GraphicTiles[th.Y, th.X].Draw(g);
                            GraphicTiles[lh.Y, lh.X].Draw(g);
                        }
                    }

                    if (!Enabled)
                        g.FillRectangle(new SolidBrush(Color.FromArgb(0xd0, Color.White)), new Rectangle(0, 0, Canvas.Width, Canvas.Height));
                }

                Canvas.Invoke(new Action(() =>
                {
                    Canvas.Image = buffer;
                }));
            });

        }
    }
}

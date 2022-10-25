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
        private KakuroBoard _board;

        public PictureBox Canvas { get; set; }
        public GraphicTile[,] GraphicTiles = { };
        public KakuroBoard Board
        {
            get {
                KakuroBoard ret = _board;

                for(int i = 0; i < SizeX; i++)
                {
                    for(int j = 0; j < SizeY; j++)
                    {
                        if (GraphicTiles[i,j] is WhiteGraphicTile)
                        {
                            (ret.Grid[i, j] as WhiteCell).Value = (GraphicTiles[i, j] as WhiteGraphicTile).DrawnNumber;
                        }
                    }
                }

                return ret;
            }
            private set
            {
                _board = value; 
            }
        }

        public Point Selected = new Point(0, 0);
        public Size Size { get; set; }
        public int SizeX { get => GraphicTiles.GetLength(1); }
        public int SizeY { get => GraphicTiles.GetLength(0); }

        // config
        public bool HighlightCurrentClues { get; set; }
        public bool HighlightWrongSums { get; set; }
        public bool HighlightWrong { get; set; }
        public bool GrayCompleteSums { get; set; }
        public bool HighlightCurrentRowColumn { get; set; }
        public bool BlueForErrors { get; set; }

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
            Board = board;

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
            try
            {
                for (int i = Selected.Y; i >= 0; i--)
                    if (GraphicTiles[i, Selected.X].Type == TileTypes.Hint)
                        return new Point(Selected.X, i);
            }
            catch { }

            return Point.Empty;
        }

        private Point LeftHintFromSelection()
        {
            try
            {
                for (int i = Selected.X; i >= 0; i--)
                if (GraphicTiles[Selected.Y, i].Type == TileTypes.Hint)
                    return new Point(i, Selected.Y);
            }
            catch { }

            return Point.Empty;
        }

        private int HorizontalSum(int y, int x)
        {
            int sum = 0;

   
            for (int i = y + 1; i < SizeY; i++)
            {
                if (GraphicTiles[x, i] is WhiteGraphicTile)
                {
                    sum += (GraphicTiles[x, i] as WhiteGraphicTile).DrawnNumber;
                }
                else
                {
                    break;
                }
            }

            return sum;
        }

        private int VerticalSum(int y, int x)
        {
            int sum = 0;

            for (int i = x + 1; i < SizeX; i++)
            {
                if (GraphicTiles[i, y] is WhiteGraphicTile)
                {
                    sum += (GraphicTiles[i, y] as WhiteGraphicTile).DrawnNumber;
                }
                else
                {
                    break;
                }
            }

            return sum;
        }

        public void AssignBoard(KakuroBoard board)
        {
            _board = board;
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
                        tiles[i, j] = new WhiteGraphicTile((cells[i, j] as WhiteCell).Value);

            Enabled = true; // якийсь баг робить щоб контроллер виключався
            GraphicTiles = tiles;

            Update();
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

        public void SetSelectedTileNumber(int number)
        {
            if (Enabled)
            {
                if (GraphicTiles[Selected.Y, Selected.X].Type == TileTypes.Number)
                    ((WhiteGraphicTile)GraphicTiles[Selected.Y, Selected.X]).DrawnNumber = number;

                Update();
            }
        }

        public void ClearNumbers()
        {
            for(int i = 0; i < SizeY; i++)
            {
                for(int j = 0; j < SizeX; j++)
                {
                    if(GraphicTiles[i, j] is WhiteGraphicTile)
                    {
                        (GraphicTiles[i, j] as WhiteGraphicTile).DrawnNumber = 0;
                    }
                }
            }

            Update();
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
                            GraphicTiles[i, j].BlueForErrors = BlueForErrors;

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
                                    numberTile.HighlightBackground = false;
                                    numberTile.Highlight = false;

                                    if (HighlightWrong && numberTile.DrawnNumber > 0 && numberTile.DrawnNumber != _board.GetHelp(i, j))
                                    {
                                        numberTile.Highlight = true;
                                    }

                                    if (HighlightCurrentRowColumn)
                                    {
                                        Point th = TopHintFromSelection();
                                        Point lh = LeftHintFromSelection();

                                        for (int k = th.Y + 1; k < SizeY; k++)
                                        {
                                            if (GraphicTiles[k, th.X] is WhiteGraphicTile)
                                                ((WhiteGraphicTile)GraphicTiles[k, th.X]).HighlightBackground = true;
                                            else break;
                                        }

                                        for (int k = lh.X + 1; k < SizeX; k++)
                                        {
                                            if (GraphicTiles[lh.Y, k] is WhiteGraphicTile)
                                                ((WhiteGraphicTile)GraphicTiles[lh.Y, k]).HighlightBackground = true;
                                            else break;
                                        }
                                    }

                                    break;
                            }

                            GraphicTiles[i, j].Draw(g);
                        }
                    }

                    if (HighlightCurrentClues)
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

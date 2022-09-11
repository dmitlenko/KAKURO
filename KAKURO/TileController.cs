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
        public PictureBox[,] BoxTiles { get { return boxTiles; } }
        public Point Selected = new Point(0,0);
        public int SizeX { get => BoxTiles.GetLength(1); }
        public int SizeY { get => BoxTiles.GetLength(0); }

        public struct Box
        {
            private PictureBox box;
            public Box(ref PictureBox b) => box = b;

            public void Move(int x, int y)
            {
                box.Left = x;
                box.Top = y;

                box.Tag = y + ":" + x;
            }

            public void Resize(int width, int height)
            {
                box.Width = width;
                box.Height = height;
            }

            public void Update()
            {
                box.Refresh();
            }
        }

        private PictureBox[,] boxTiles;
        private Point PrevSelected = new Point(0, 0);

        private bool _enabled = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                if (value) EnableTiles();
                else DisableTiles();
            }
        }

        public Box this[int x, int y] { get => new Box(ref boxTiles[y, x]); }

        public TileController(PictureBox[,] tiles)
        {
            boxTiles = tiles;

            PrepareBoxTiles();

            BorderSelected();
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            string[] coords = ((PictureBox)sender).Tag.ToString().Split(':');
            int y = Convert.ToInt32(coords[0]);
            int x = Convert.ToInt32(coords[1]);

            // Записати координати в точку
            Selected = new Point(x,y);

            BorderSelected();
        }

        private void PrepareBoxTiles()
        {
            for (int i = 0; i < boxTiles.GetLength(0); i++)
            {
                for (int j = 0; j < boxTiles.GetLength(1); j++)
                {
                    boxTiles[j, i].BorderStyle = BorderStyle.None; // Прибираємо краї у PictureBox'у
                    boxTiles[j, i].BackColor = Color.Black; // Задаємо прозорий фон
                    boxTiles[j, i].Click += new EventHandler(Tile_Click); // Додаємо подію кліку
                    boxTiles[j, i].Tag = j + ":" + i; // Додаємо тег з координатами
                }
            }
        }

        private void BorderSelected()
        {
            boxTiles[PrevSelected.Y, PrevSelected.X].BorderStyle = BorderStyle.None;
            boxTiles[Selected.Y, Selected.X].BorderStyle = BorderStyle.FixedSingle;
            PrevSelected = new Point(Selected.X, Selected.Y);
        }

        private void DisableTiles() // Вимкнути всі тайли
        {
            for (int i = 0; i < boxTiles.GetLength(0); i++)
                for (int j = 0; j < boxTiles.GetLength(1); j++)
                    boxTiles[j, i].Enabled = false;
        }

        private void EnableTiles() // Ввімкнути всі тайли
        {
            for (int i = 0; i < boxTiles.GetLength(0); i++)
                for (int j = 0; j < boxTiles.GetLength(1); j++)
                    boxTiles[j, i].Enabled = true;
        }

        public void AssignTiles(GraphicTile[,] tiles)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                    tiles[i,j].Canvas = boxTiles[i,j];
        }

        public void MoveSelectionUp()
        {
            if (Enabled)
            {
                if (Selected == Point.Empty) Selected = new Point(0, 0);

                if (Selected.Y > 0) Selected.Y -= 1;
                BorderSelected();
            }
        }

        public void MoveSelectionDown()
        {
            if (Enabled)
            {
                if (Selected == Point.Empty) Selected = new Point(0, 0);

                if (Selected.Y < boxTiles.GetLength(0) - 1) Selected.Y += 1;
                BorderSelected();
            }
        }

        public void MoveSelectionLeft()
        {
            if (Enabled)
            {
                if (Selected == Point.Empty) Selected = new Point(0, 0);

                if (Selected.X > 0) Selected.X -= 1;
                BorderSelected();
            }
        }

        public void MoveSelectionRight()
        {
            if (Enabled)
            {
                if (Selected == Point.Empty) Selected = new Point(0, 0);

                if (Selected.X < boxTiles.GetLength(1) - 1) Selected.X += 1;
                BorderSelected();
            }
        }
    }
}

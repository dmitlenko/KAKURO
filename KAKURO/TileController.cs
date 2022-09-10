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
        private PictureBox[,] boxTiles;
        public PictureBox[,] BoxTiles { get { return boxTiles; } }
        public Point Selected = new Point(0,0);
        private Point PrevSelected = new Point(0, 0);

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
                    boxTiles[j, i].BackColor = Color.Transparent; // Задаємо прозорий фон
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

        public void DisableTiles() // Вимкнути всі тайли
        {
            for (int i = 0; i < boxTiles.GetLength(0); i++)
                for (int j = 0; j < boxTiles.GetLength(1); j++)
                    boxTiles[j, i].Enabled = false;
        }

        public void EnableTiles() // Ввімкнути всі тайли
        {
            for (int i = 0; i < boxTiles.GetLength(0); i++)
                for (int j = 0; j < boxTiles.GetLength(1); j++)
                    boxTiles[j, i].Enabled = true;
        }

        public void AssignTiles(ref GraphicTile[,] tiles)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
                for (int y = 0; y < tiles.GetLength(1); y++)
                    tiles[y, x].Picture = boxTiles[y, x];
        }

        public void MoveSelectionUp()
        {
            if (Selected == Point.Empty) Selected = new Point(0, 0);

            if (Selected.Y > 0) Selected.Y -= 1;
            BorderSelected();
        }

        public void MoveSelectionDown()
        {
            if (Selected == Point.Empty) Selected = new Point(0, 0);

            if (Selected.Y < boxTiles.GetLength(0) - 1) Selected.Y += 1;
            BorderSelected();
        }

        public void MoveSelectionLeft()
        {
            if (Selected == Point.Empty) Selected = new Point(0, 0);

            if (Selected.X > 0) Selected.X -= 1;
            BorderSelected();
        }

        public void MoveSelectionRight()
        {
            if (Selected == Point.Empty) Selected = new Point(0, 0);

            if (Selected.X < boxTiles.GetLength(1) - 1) Selected.X += 1;
            BorderSelected();
        }
    }
}

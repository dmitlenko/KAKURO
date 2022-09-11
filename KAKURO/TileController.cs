﻿using System;
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

        private PictureBox[,] boxTiles;
        private Point PrevSelected = new Point(0, 0);

        private bool _enabled = true;

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                if (value) EnableTiles();
                else DisableTiles();
            }
        }

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

        public void AssignTiles(ref GraphicTile[,] tiles)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
                for (int y = 0; y < tiles.GetLength(1); y++)
                    tiles[y, x].Picture = boxTiles[y, x];
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

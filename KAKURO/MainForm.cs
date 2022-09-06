using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    public partial class MainForm : Form
    {
        private DateTime CurrentTime = new DateTime(1, 1, 1, 0, 0, 0);
        private int CurrentSeed { get; set; }
        private bool Saved { get; set; }
        private Point SelectedTile = new Point(0, 0);
        private PictureBox[,] tiles;

        public MainForm()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Оновлюємо час кожну секунду
            statusTime.Text = CurrentTime.ToString("HH:mm:ss");
            CurrentTime = CurrentTime.AddSeconds(1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Генеруємо сід
            Random rnd = new Random();
            CurrentSeed = rnd.Next();
            
            // Виводимо сід на екран
            statusSeed.Text = "Seed: " + CurrentSeed.ToString();

            // Просвоюємо змінній Saved "false" так як гра ще не збережена
            Saved = false;

            // Підготувати клітинки
            PrepareTiles();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Saved) Application.Exit();
            else
            {
                // Запитати у користувача чи точно він хоче вийти з гри
                DialogResult res = MessageBox.Show("Результат поточної гри не буде збережено.", "Ви дійсно хочете вийти з гри?", MessageBoxButtons.YesNo);
                // Якщо так, то вийти
                if (res == DialogResult.Yes) Application.Exit();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                // Запитати у користувача чи точно він хоче вийти з гри
                DialogResult res = MessageBox.Show("Результат поточної гри не буде збережено.", "Ви дійсно хочете вийти з гри?", MessageBoxButtons.YesNo);
                // Якщо так, то вийти
                e.Cancel = (res == DialogResult.No);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Point current = new Point(SelectedTile.X, SelectedTile.Y);
            
            switch(e.KeyCode)
            {
                case Keys.Up:
                    if ((SelectedTile.Y - 1) > -1 && SelectedTile.Y < tiles.GetLength(0)) SelectedTile.Y -= 1;
                    break;
                case Keys.Down:
                    if ((SelectedTile.Y + 1) < tiles.GetLength(0)) SelectedTile.Y += 1;
                    break;
                case Keys.Left:
                    if ((SelectedTile.X - 1) > -1) SelectedTile.X -= 1;
                    break;
                case Keys.Right:
                    if ((SelectedTile.X + 1) < tiles.GetLength(1)) SelectedTile.X += 1;
                    break;
            }

            if (current.X > -1 && current.Y > -1)
            {
                if(tiles[current.Y, current.X].BorderStyle == BorderStyle.FixedSingle) tiles[current.Y, current.X].BorderStyle = BorderStyle.None;
                tiles[SelectedTile.Y, SelectedTile.X].BorderStyle = BorderStyle.FixedSingle;
            }
                
        }

        private void PrepareTiles()
        {

            tiles = new PictureBox[,]
            {
                {tile0_0, tile0_1, tile0_2, tile0_3, tile0_4, tile0_5, tile0_6, tile0_7},
                {tile1_0, tile1_1, tile1_2, tile1_3, tile1_4, tile1_5, tile1_6, tile1_7},
                {tile2_0, tile2_1, tile2_2, tile2_3, tile2_4, tile2_5, tile2_6, tile2_7},
                {tile3_0, tile3_1, tile3_2, tile3_3, tile3_4, tile3_5, tile3_6, tile3_7},
                {tile4_0, tile4_1, tile4_2, tile4_3, tile4_4, tile4_5, tile4_6, tile4_7},
                {tile5_0, tile5_1, tile5_2, tile5_3, tile5_4, tile5_5, tile5_6, tile5_7},
                {tile6_0, tile6_1, tile6_2, tile6_3, tile6_4, tile6_5, tile6_6, tile6_7},
                {tile7_0, tile7_1, tile7_2, tile7_3, tile7_4, tile7_5, tile7_6, tile7_7}
            };

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    tiles[i, j].BorderStyle = BorderStyle.None;
                    tiles[i, j].Click += new EventHandler(Tile_Click);
                    tiles[i, j].Tag = i + ":" + j;
                }
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Point current = new Point(SelectedTile.X, SelectedTile.Y);
            string[] coords = ((PictureBox)sender).Tag.ToString().Split(':');
            int x = Convert.ToInt32(coords[0]);
            int y = Convert.ToInt32(coords[1]);

            if (current.X > -1 && current.Y > -1)
            {
                if (tiles[current.Y, current.X].BorderStyle == BorderStyle.FixedSingle) tiles[current.Y, current.X].BorderStyle = BorderStyle.None;
                ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;


            }
        }
    }
}

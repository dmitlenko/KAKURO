using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAKURO
{
    public partial class MainForm : Form
    {
        private DateTime CurrentTime = new DateTime();
        private bool Saved = false;
        private bool _paused = false;
        private TileController tileController;
        private PictureBox[,] boxTiles;

        private bool Paused
        {
            get => _paused;
            set
            {
                _paused = value;
                if (value)
                {
                    statusInPause.Visible = true;
                    tileController.Enabled = false;
                    pauseToolStripMenuItem.Enabled = false;
                    resumeToolStripMenuItem.Enabled = true;
                } else
                {
                    statusInPause.Visible = false;
                    tileController.Enabled = true;
                    pauseToolStripMenuItem.Enabled = true;
                    resumeToolStripMenuItem.Enabled = false;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!Paused)
            {
                // Оновлюємо час кожну секунду
                statusTime.Text = CurrentTime.ToString("HH:mm:ss");
                CurrentTime = CurrentTime.AddSeconds(1);
            } else
            {
                statusInPause.Visible = !statusInPause.Visible;
            }
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Підготувати тайли
            // Я це все руками писав :|
            boxTiles = new PictureBox[,]
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

            tileController = new TileController(boxTiles);

            MainForm_ResizeEnd(sender, e);

            BlackGraphicTile blackGraphicTile = new BlackGraphicTile(tile0_0);
            blackGraphicTile.Draw();

            HintGraphicTile hintGraphicTile = new HintGraphicTile(tile0_1, 24, 44);
            hintGraphicTile.Draw();

            NumberGraphicTile numberGraphicTile = new NumberGraphicTile(tile0_2, 4);
            numberGraphicTile.Draw();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            switch (e.KeyCode)
            {
                case Keys.Up: // При натисканні на стрілку вгору здвинути координату вгору
                    tileController.MoveSelectionUp();
                    break;
                case Keys.Down: // При натисканні на стрілку вниз здвинути координату вниз
                    tileController.MoveSelectionDown();
                    break;
                case Keys.Left: // При натисканні на стрілку вліво здвинути координату вліво
                    tileController.MoveSelectionLeft();
                    break;
                case Keys.Right: // При натисканні на стрілку вправо здвинути координату вправо
                    tileController.MoveSelectionRight();
                    break;
            }
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Відкрити форму з правилами
            new RulesForm().Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generator generator = new Generator();
            Cell[,] board = generator.GenerateBoard(6, 6, 0.2);
            GraphicTile[,] tiles = generator.CellsToGraphicTiles(board);

            tileController.AssignTiles(ref tiles);
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paused = true;
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paused = false;
        }

        FormWindowState LastWindowState = FormWindowState.Minimized;
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;
                if (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal)
                {
                    MainForm_ResizeEnd(sender , e);
                }
            }   
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            int tileHW = tilesPanel.Height / 8;
            int tileStartX = (tilesPanel.Width / 2) - tileHW * 4;

            for (int i = 0; i < tileController.SizeY; i++)
            {
                for (int j = 0; j < tileController.SizeX; j++)
                {
                    tileController[i,j].Move(tileStartX + (tileHW * i), (tileHW * j));
                    tileController[i,j].Resize(tileHW, tileHW);
                    tileController[i,j].Update();
                }
            }
        }
    }
}

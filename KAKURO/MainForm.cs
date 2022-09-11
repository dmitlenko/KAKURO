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
        private Size MinSize = new Size(386, 386);

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
            tileController = new TileController(canvas, 8, 8, new GraphicTile[,] { 
                { new BlackGraphicTile(),  new HintGraphicTile(21, 33),  new NumberGraphicTile(4), new BlackGraphicTile(),new BlackGraphicTile()},
                { new BlackGraphicTile(), new BlackGraphicTile(), new NumberGraphicTile(1),  new NumberGraphicTile(2),  new NumberGraphicTile(3)},
            });
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
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    tileController.SetTileNumber(e.KeyValue - 48);
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
            tileController.AssignTiles(generator.CellsToGraphicTiles(board));
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
            int padding = 8;
            int paddX2 = padding * 2;

            if (canvasPanel.Width - paddX2 < canvas.Height || canvasPanel.Height - paddX2 > canvasPanel.Width)
            {
                canvas.Height = canvasPanel.Width - paddX2;
                canvas.Width = canvasPanel.Width - paddX2;
                canvas.Left = padding;
                canvas.Top = (canvasPanel.Height / 2) - (canvas.Height / 2);
            }
            else
            {
                canvas.Height = canvasPanel.Height - paddX2;
                canvas.Width = canvasPanel.Height - paddX2;
                canvas.Top = padding;
                canvas.Left = (canvasPanel.Width / 2) - (canvas.Width / 2);
            }

            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;

                if (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal)
                {
                    tileController.Update();
                }
            }
        }

        private void UpdateCanvasEvent(object sender, EventArgs e)
        {
            tileController.Update();
        }
    }
}

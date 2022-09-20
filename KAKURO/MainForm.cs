using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro
{
    public partial class MainForm : Form
    {
        private DateTime CurrentTime = new DateTime();
        private bool Saved = false;
        private bool _paused = false;
        private TileController tileController;
        private BoardGenerator generator;

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
                    pauseToolStripMenuItem.Enabled = pauseToolStripButton.Enabled = false;
                    resumeToolStripMenuItem.Enabled = resumeToolStripButton.Enabled = true;
                } else
                {
                    statusInPause.Visible = false;
                    tileController.Enabled = true;
                    pauseToolStripMenuItem.Enabled = pauseToolStripButton.Enabled = true;
                    resumeToolStripMenuItem.Enabled = resumeToolStripButton.Enabled = false;
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

            tileController.Update();
        }

        private void LoadSettings()
        {
            tileController.LoadSettings();

            statusTime.Visible = !Properties.Settings.Default.HideTimer;
        }

        private void CreateNewGame()
        {
            Paused = false;

            generator.GenerateBoard(Properties.Settings.Default.BoardWidth - 2, Properties.Settings.Default.BoardHeight - 2, 0.3, () =>
            {
                tileController.AssignCells(generator.Cells());
                CurrentTime = new DateTime();
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            generator = new BoardGenerator();
            tileController = new TileController(canvas);

            CreateNewGame();
            LoadSettings();
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
                    Saved = false;
                    break;
                case Keys.D0:
                case Keys.Delete:
                    tileController.SetTileNumber(0);
                    Saved = false;
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

            LoadSettings();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGame();
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

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            tileController.Update();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Saved && MessageBox.Show("Результат поточної гри не буде збережено.", "Відкрити збережену гру", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GameSave save = (GameSave)Serealizer.Deserialize(openFileDialog.FileName);

                    if (save.Valid())
                    {
                        tileController.Size = save.Size;
                        tileController.AssignCells(save.Cells);
                        tileController.Selected = save.Selection;
                        tileController.Enabled = true;
                        tileController.Update();

                        CurrentTime = save.Time;
                        Saved = true;
                    } else
                    {
                        throw new Exception();
                    }
                } catch (Exception)
                {
                    if (Path.GetExtension(openFileDialog.FileName) == ".kpf")
                        MessageBox.Show("Файл гри пошкоджений або застарілий.", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Не вдалося зчитати файл.", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Serealizer.Serialize(new GameSave(tileController.CellData(), CurrentTime, tileController.Size, tileController.Selected), saveFileDialog.FileName);

                Saved = true;
            }
        }

        private void restartToolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTime = new DateTime();
            tileController.AssignCells(generator.Cells());
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tileController.AssignCells(generator.Cells(true));
        }
    }
}

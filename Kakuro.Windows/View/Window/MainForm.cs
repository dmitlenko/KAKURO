using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Core;
using Kakuro.Engine.Graphics;
using Kakuro.Windows.View.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro.Windows
{
    public partial class MainForm : Form
    {
        Renderer rend;
        KakuroBoard kakuroBoard;
        Time solveTime = new Time(0,0,0);

        int kwidth = 7, kheight = 7;
        bool autoSubmit;

        public MainForm()
        {
            InitializeComponent();

            rend = new Renderer(pictureBox1);
            kakuroBoard = new KakuroBoard(7, 7);
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            int padding = 8;

            int height = 337;
            int width = 337;

            int top = panel1.Height/2 - height/2 - padding;
            int left = panel1.Width / 2 - width / 2;

            pictureBox1.Size = new Size(width, height);
            pictureBox1.Location = new Point(left, top);

            rend.Update();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Hide();
            new MainForm().ShowDialog();
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            solveTime.IncreaseSecond();

            timeLabel.Text = solveTime.ToString();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to restart current game?", "Are you sure?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                pictureBox1.Invoke(new Action(() =>
                {
                    rend.ClearNumbers();
                    solveTime = new Time();
                }));
            }
        }

        private void LoadSettings()
        {
            autoSubmit = (bool)Properties.Settings.Default["AutoSubmit"];
            rend.BlueForErrors = (bool)Properties.Settings.Default["BlueForErrors"];
            rend.GrayCompleteSums = (bool)Properties.Settings.Default["GrayCompleteSums"];
            timeLabel.Visible = !(bool)Properties.Settings.Default["HideTimer"];
            rend.HighlightCurrentClues = (bool)Properties.Settings.Default["HighlightCurrentClues"];
            rend.HighlightCurrentRowColumn = (bool)Properties.Settings.Default["HighlightCurrentRowCol"];
            rend.HighlightWrong = (bool)Properties.Settings.Default["HighlightWrongCells"];
            rend.HighlightWrongSums = (bool)Properties.Settings.Default["HighlightWrongSums"];
            numberPanelBox.Visible = (bool)Properties.Settings.Default["ShowNumberButtons"];

            MainForm_ResizeEnd(null, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();

            Task.Factory.StartNew(() =>
            {
                Generator g = new Generator();
                kakuroBoard = g.Generate(kwidth, kheight, 1);

                pictureBox1.Invoke(new Action(() =>
                {
                    toolStrip1.Enabled = true;
                    timer1.Enabled = true;

                    rend.AssignBoard(kakuroBoard);
                    rend.Update();
                }));
            });
        }

        private bool CheckKakuro()
        {
            return CountUnnasigned() == 0;
        }

        private int CountUnnasigned()
        {
            int count = 0;

            for (int i = 0; i < kheight; i++)
            {
                for (int j = 0; j < kwidth; j++)
                {
                    if (rend.GraphicTiles[j, i] is WhiteGraphicTile &&
                        (rend.GraphicTiles[j, i] as WhiteGraphicTile).DrawnNumber != kakuroBoard.GetHelp(i,j))
                        count++;
                }
            }

            return count;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (CheckKakuro())
            {
                timer1.Enabled = false;
                MessageBox.Show(string.Format("Congrats! You solved the puzzle in {0}!", solveTime.ToString()));
            } else
            {
                MessageBox.Show(string.Format("You not solved the puzzle. Still {0} cells to go!", CountUnnasigned()));
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    rend.MoveSelectionUp();
                    break;
                case Keys.Down:
                    rend.MoveSelectionDown();
                    break;
                case Keys.Left:
                    rend.MoveSelectionLeft();
                    break;
                case Keys.Right:
                    rend.MoveSelectionRight();
                    break;
                case Keys.D0: case Keys.D1: case Keys.D2: case Keys.D3: case Keys.D4: 
                case Keys.D5: case Keys.D6: case Keys.D7: case Keys.D8: case Keys.D9:
                    rend.SetSelectedTileNumber(e.KeyValue - 48);
                    if (autoSubmit && CheckKakuro())
                    {
                        timer1.Enabled = false;
                        MessageBox.Show(string.Format("Congrats! You solved the puzzle in {0}!", solveTime.ToString()));
                    }
                    break;
                case Keys.Delete:
                    rend.SetSelectedTileNumber(0);
                    break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            rend.SetSelectedTileNumber(Convert.ToInt32((sender as Button).Text));
            if(autoSubmit && CheckKakuro())
            {
                timer1.Enabled = false;
                MessageBox.Show(string.Format("Congrats! You solved the puzzle in {0}!", solveTime.ToString()));
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            int padding = 8;

            int height = numberPanel.Height; 
            int width = numberPanel.Width;

            int top = numberPanelBox.Height / 2 - height / 2 - padding;
            int left = numberPanelBox.Width / 2 - width / 2;

            //numberPanel.Size = new Size(width, height);
            numberPanel.Location = new Point(left, top);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            timer1.Enabled = false;
            settingsDialog.ShowDialog();
            timer1.Enabled = true;
            LoadSettings();
            rend.Update();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (rend.GraphicTiles.GetLength(0) <= 0 && rend.GraphicTiles.GetLength(1) <= 0) return;

            for(int i = 0; i < kwidth; i++)
            {
                for(int j = 0; j < kheight; j++)
                {
                    if (rend.GraphicTiles[j,i] is WhiteGraphicTile)
                    {
                        (rend.GraphicTiles[j, i] as WhiteGraphicTile).DrawnNumber = kakuroBoard.GetHelp(j, i);
                    }
                }
            }

            rend.Update();

            if (autoSubmit && CheckKakuro())
            {
                timer1.Enabled = false;
                MessageBox.Show(string.Format("Congrats! You solved the puzzle in {0}!", solveTime.ToString()));
            }
        }
    }
}

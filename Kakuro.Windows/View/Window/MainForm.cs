using Kakuro.Engine.Algorithms;
using Kakuro.Engine.Cells;
using Kakuro.Engine.Core;
using Kakuro.Engine.Graphics;
using Kakuro.Windows.Core;
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
        CheckPointFile checkPoint = new CheckPointFile();

        int kwidth = 7, kheight = 7;
        bool autoSubmit;

        public MainForm()
        {
            InitializeComponent();

            rend = new Renderer(mainCanvas);
            kakuroBoard = new KakuroBoard(7, 7);
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            int pad = 24, h, w, tx, ty;

            int hc = panel1.Height, wc = panel1.Width;

            if (hc > wc)
            {
                w = wc - pad * 2;
                h = w;
                tx = pad;
                ty = (hc / 2) - (h / 2);
            } else
            {
                h = hc - pad * 5;
                w = h;
                tx = wc / 2 - w / 2;
                ty = pad;
            }

            numberPanel.Left = numberPanelBox.Width / 2 - numberPanel.Width / 2;

            mainCanvas.Size = new Size(w, h);
            mainCanvas.Location = new Point(tx, ty);
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
                rend.ClearNumbers();
                solveTime = new Time();
                timer.Enabled = true;
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
                int d = (int)Properties.Settings.Default["BoardDifficulty"];
                int[] dd = { 5, 6, 7 };
                (kwidth, kheight) = (dd[d - 1], dd[d - 1]);
                kakuroBoard = g.Generate(kwidth,kheight, d);

                mainCanvas.Invoke(new Action(() =>
                {
                    toolStrip.Enabled = true;
                    timer.Enabled = true;

                    rend.AssignBoard(kakuroBoard);
                    rend.Update();
                }));
            });
        }

        private bool CheckKakuro()
        {
            for(int i = 0; i < kheight; i++)
            {
                for(int j = 0; j < kwidth; j++)
                {
                    if (rend.GraphicTiles[i, j] is SumGraphicTile)
                    {
                        int sumv = 0, vsum = (rend.GraphicTiles[i, j] as SumGraphicTile).SumVertical;
                        int sumh = 0, hsum = (rend.GraphicTiles[i, j] as SumGraphicTile).SumHorizontal;

                        vsum = vsum == -1 ? 0 : vsum;
                        hsum = hsum == -1 ? 0 : hsum;

                        for(int k = i + 1; k < kheight; k++)
                        {
                            if (rend.GraphicTiles[k, j] is WhiteGraphicTile)
                            {
                                sumv += (rend.GraphicTiles[k, j] as WhiteGraphicTile).DrawnNumber;
                            }
                            else break;
                        }

                        for (int k = j + 1; k < kheight; k++)
                        {
                            if (rend.GraphicTiles[i, k] is WhiteGraphicTile)
                            {
                                sumh += (rend.GraphicTiles[i, k] as WhiteGraphicTile).DrawnNumber;
                            }
                            else break;
                        }

                        if (sumv != vsum || sumh != hsum) return false;
                    }
                }
            }


            return true;
        }

        private int CountUnnasigned()
        {
            int count = 0;

            for (int i = 0; i < kheight; i++)
            {
                for (int j = 0; j < kwidth; j++)
                {
                    if (rend.GraphicTiles[i,j] is WhiteGraphicTile &&
                        (rend.GraphicTiles[i,j] as WhiteGraphicTile).DrawnNumber != kakuroBoard.GetHelp(i,j))
                        count++;
                }
            }

            return count;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (CheckKakuro())
            {
                timer.Enabled = false;
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
                        timer.Enabled = false;
                        MessageBox.Show(string.Format("Congrats! You solved the puzzle in {0}!", solveTime.ToString()));
                    }
                    break;
                case Keys.Delete:
                    rend.SetSelectedTileNumber(0);
                    break;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            rend.SetSelectedTileNumber(Convert.ToInt32((sender as Button).Text));
            if(autoSubmit && CheckKakuro())
            {
                timer.Enabled = false;
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

            numberPanel.Location = new Point(left, top);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SettingsDialog settingsDialog = new SettingsDialog();
            timer.Enabled = false;
            settingsDialog.ShowDialog();
            timer.Enabled = true;
            LoadSettings();
            rend.Update();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            CheckpointDialog checkpointDialog = new CheckpointDialog(checkPoint, rend.Board, solveTime);

            timer.Enabled = false;

            if (checkpointDialog.ShowDialog() == DialogResult.OK)
            {
                solveTime = checkpointDialog.Loaded.Time;
                kakuroBoard = checkpointDialog.Loaded.Instance;

                rend.AssignBoard(kakuroBoard);
                rend.Update();
            }

            timer.Enabled = true;
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
                timer.Enabled = false;
                MessageBox.Show(string.Format("Congrats! You solved the puzzle in {0}!", solveTime.ToString()));
            }
        }
    }
}

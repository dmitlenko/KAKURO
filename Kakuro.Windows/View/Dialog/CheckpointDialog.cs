using Kakuro.Engine.Core;
using Kakuro.Windows.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro.Windows.View.Dialog
{
    public partial class CheckpointDialog : Form
    {
        CheckPointFile checkPoint;
        public CheckPoint Loaded { get; private set; }
        public KakuroBoard Instance { get; private set; }
        public Time Time { get; private set; }

        public CheckpointDialog(CheckPointFile file, KakuroBoard instance, Time time)
        {
            InitializeComponent();
            checkPoint = file;

            Instance = instance;
            Time = time;
        }

        private void CheckpointDialog_Load(object sender, EventArgs e)
        {
            checkPointList.Items.Clear();
            foreach (var item in checkPoint.CheckPoints)
            {
                checkPointList.Items.Add(item.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkPointList.SelectedIndex >= 0 && MessageBox.Show("Do you want to load this checkpoint?", "Are you sure?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                Loaded = checkPoint.CheckPoints[checkPointList.SelectedIndex];
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkPoint.CheckPoints.Add(new CheckPoint(
                "Checkpoint " + (checkPoint.CheckPoints.Count + 1),
                Time, Instance, DateTime.Now));
            checkPoint.Save();
            CheckpointDialog_Load(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(checkPointList.SelectedIndex >= 0 && MessageBox.Show("Do you want to delete this checkpoint?", "Are you sure?", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                checkPoint.CheckPoints.RemoveAt(checkPointList.SelectedIndex);
                checkPoint.Save();
                CheckpointDialog_Load(null, null);
            }
        }
    }
}

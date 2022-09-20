using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kakuro
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            boardWidth.Value = Properties.Settings.Default.BoardWidth;
            boardHeight.Value = Properties.Settings.Default.BoardHeight;
            autoSubmitCheck.Checked = Properties.Settings.Default.AutoSubmit;
            highlightDuplicatesCheck.Checked = Properties.Settings.Default.HighlightDuplicates;
            highlightSelectionSumsCheck.Checked = Properties.Settings.Default.HighlightSelectionSums;
            highlightWrongSumsCheck.Checked = Properties.Settings.Default.HighlightWrongSums;
            grayCompleteSumsCheck.Checked = Properties.Settings.Default.GrayCompleteSums;
            hideTimerCheck.Checked = Properties.Settings.Default.HideTimer;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BoardWidth = (int) boardWidth.Value;
            Properties.Settings.Default.BoardHeight = (int) boardHeight.Value;
            Properties.Settings.Default.AutoSubmit = autoSubmitCheck.Checked;
            Properties.Settings.Default.HighlightDuplicates = highlightDuplicatesCheck.Checked;
            Properties.Settings.Default.HighlightSelectionSums = highlightSelectionSumsCheck.Checked;
            Properties.Settings.Default.HighlightWrongSums = highlightWrongSumsCheck.Checked;
            Properties.Settings.Default.GrayCompleteSums = grayCompleteSumsCheck.Checked;
            Properties.Settings.Default.HideTimer = hideTimerCheck.Checked;

            Properties.Settings.Default.Save();
            Close();
        }
    }
}

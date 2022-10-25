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
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            autoSubmit.Checked = (bool) Properties.Settings.Default["AutoSubmit"];
            blueForErrors.Checked = (bool)Properties.Settings.Default["BlueForErrors"];
            grayCompletedNumbers.Checked = (bool)Properties.Settings.Default["GrayCompleteSums"];
            hideTimer.Checked = (bool)Properties.Settings.Default["HideTimer"];
            highlightCurrentClues.Checked = (bool)Properties.Settings.Default["HighlightCurrentClues"];
            highlightCurrentRowSum.Checked = (bool)Properties.Settings.Default["HighlightCurrentRowCol"];
            highlightWrongCells.Checked = (bool)Properties.Settings.Default["HighlightWrongCells"];
            highlightWrongSums.Checked = (bool)Properties.Settings.Default["HighlightWrongSums"];
            showNumberButtons.Checked = (bool)Properties.Settings.Default["ShowNumberButtons"];
        }

        private void save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoSubmit = autoSubmit.Checked;
            Properties.Settings.Default.BlueForErrors = blueForErrors.Checked;
            Properties.Settings.Default.GrayCompleteSums = grayCompletedNumbers.Checked;
            Properties.Settings.Default.HideTimer = hideTimer.Checked;
            Properties.Settings.Default.HighlightCurrentClues = highlightCurrentClues.Checked;
            Properties.Settings.Default.HighlightCurrentRowCol = highlightCurrentRowSum.Checked;
            Properties.Settings.Default.HighlightWrongCells = highlightWrongCells.Checked;
            Properties.Settings.Default.HighlightWrongSums = highlightWrongSums.Checked;
            Properties.Settings.Default.ShowNumberButtons = showNumberButtons.Checked;

            Close();
        }
    }
}

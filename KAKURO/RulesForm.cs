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
    public partial class RulesForm : Form
    {
        public RulesForm()
        {
            InitializeComponent();
        }

        private void RulesForm_Load(object sender, EventArgs e)
        {
            string html = Properties.Resources.rules;
            browser.Navigate("about:blank");
            HtmlDocument doc = browser.Document;
            doc.Write(String.Empty);
            browser.DocumentText = html;
        }
    }
}

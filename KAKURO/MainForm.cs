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
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Saved) Application.Exit();
            else
            {
                // Запитати у користувача чи точно він хоче вийти з гри
                DialogResult res = MessageBox.Show("Ви дійсно хочете вийти з гри?", "Результат поточної гри не буде збережено.", MessageBoxButtons.YesNo);
                // Якщо так, то вийти
                if (res == DialogResult.Yes) Application.Exit();
            }
        }
    }
}

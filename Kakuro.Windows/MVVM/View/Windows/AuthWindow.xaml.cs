using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Kakuro.Engine.Authorization;
using Kakuro.Engine.Rankings;


namespace Kakuro.Windows
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();

            SerealizedRanksList ranker = new SerealizedRanksList();

            /*ranker.Add(new UserRank(0, 10, 10, 10));
            ranker.Add(new UserRank(1, 10, 10, 10));
            ranker.Add(new UserRank(2, 10, 10, 10));*/

            for (int i = 0; i < ranker.Count; MessageBox.Show(ranker[i++].TimeString())) ;

            ranker.Save();
        }
    }
}

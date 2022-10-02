using Kakuro.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kakuro.Windows.MVVM.ViewModel
{
    class HomeViewModel: ObservableObject
    {
        public RelayCommand PlayButtonClick { get; set; }

        public HomeViewModel()
        {
            PlayButtonClick = new RelayCommand(o =>
            {
                MessageBox.Show("This shit is working!");
            });
        }
    }
}

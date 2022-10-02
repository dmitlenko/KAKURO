using Kakuro.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Windows.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand RatingsViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }
        public RelayCommand OtherViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public RatingsViewModel RatingsVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public OtherViewModel OtherVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            RatingsVM = new RatingsViewModel();
            SettingsVM = new SettingsViewModel();
            OtherVM = new OtherViewModel();

            _currentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => CurrentView = HomeVM);
            RatingsViewCommand = new RelayCommand(o => CurrentView = RatingsVM);
            SettingsViewCommand = new RelayCommand(o => CurrentView = SettingsVM);
            OtherViewCommand = new RelayCommand(o => CurrentView = OtherVM);
        }
    }
}

using Kakuro.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kakuro.Windows.MVVM.ViewModel
{
    class AuthViewModel : ObservableObject
    {
        public LoginViewModel LoginVM { get; set; }
        public RegisterViewModel RegisterVM { get; set; }

        public RelayCommand LoginCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

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

        public AuthViewModel()
        {
            LoginVM = new LoginViewModel();
            RegisterVM = new RegisterViewModel();

            LoginCommand = new RelayCommand(o =>
            {
                CurrentView = LoginVM;
            });

            RegisterCommand = new RelayCommand(o =>
            {
                CurrentView = RegisterVM;
            });

            CurrentView = LoginVM;
        }
    }
}

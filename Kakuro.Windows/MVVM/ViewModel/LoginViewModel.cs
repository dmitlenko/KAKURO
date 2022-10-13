using Kakuro.Engine.Authorization;
using Kakuro.Windows.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kakuro.Windows.MVVM.ViewModel
{
    class LoginViewModel: ObservableObject
    {
        private string _username;
        public string UserName
        {
            get { return _username; }
            set 
            {
                if (!string.Equals(this._username, value))
                {
                    _username = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (!string.Equals(this._password, value))
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public RelayCommand LoginCommand { get; set; } 

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(o =>
            {
                Authenticator aut = new Authenticator();

                if (aut.Authorize(UserName, Password))
                {
                    Application.Current.Windows[0].Hide();
                    new MainWindow().Show();
                    Application.Current.Windows[0].Close();
                } else
                {
                    MessageBox.Show("Невірні дані для входу", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, o => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password));


        }
    }
}

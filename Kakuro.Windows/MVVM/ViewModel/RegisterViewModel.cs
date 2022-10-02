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
    class RegisterViewModel: ObservableObject
    {
        public RelayCommand RegisterCommand { get; set; }

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set 
            { 
                displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private bool CheckDisplayName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length != 0 && name.Length <= 32;
        }

        private void Register()
        {
            Authenticator aut = new Authenticator();
            try
            {
                if (!aut.CheckUsername(UserName)) throw new Exception("Невірний формат імені користувача!");
                if (!aut.CheckPassword(Password)) throw new Exception("Невірний формат паролю користувача!");
                if (!CheckDisplayName(DisplayName)) throw new Exception("Невірний формат повного імені!");

                if(aut.Register(DisplayName, UserName, Password))
                {
                    MessageBox.Show("Реєстрація завершена. Будь ласка, увійдіть.", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    Password = UserName = DisplayName = "";
                }
                    
            } catch(Exception e)
            {
                MessageBox.Show(e.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(o => Register());
        }
    }
}

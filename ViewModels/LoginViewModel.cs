using ChatWithMeWindows.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatWithMeWindows.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthenticationService _authService;
        private string _username;
        private string _password;
        private string _loginStatus;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel(AuthenticationService authService)
        {
            _authService = authService;
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            //RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string LoginStatus { 
            get => _loginStatus;
            set { _loginStatus = value; OnPropertyChanged(); }
        }
        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            try
            {
                var user = await _authService.LoginAsync(Username, Password);
                if (user != null && !string.IsNullOrEmpty(user.Token))
                {
                    LoginStatus = "Login successful";
                    //TODO save token and nav to chat screen
                }
            }
            catch (Exception ex)
            {
                LoginStatus = $"Login failed: {ex.Message}";
                Console.WriteLine(ex.ToString());
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
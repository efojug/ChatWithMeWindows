using ChatWithMeWindows.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Views
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; }
        public LoginPage()
        {
            this.InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(LoginViewModel)) as LoginViewModel;
            DataContext = ViewModel;
        }

        private void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = PasswordBox.Password;
        }
    }
}

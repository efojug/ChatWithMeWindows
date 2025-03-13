using ChatWithMeWindows.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithMeWindows.Views
{
    public sealed partial class ChatPage : Page
    {
        public ChatViewModel ViewModel { get; }
        public ChatPage()
        {
            this.InitializeComponent();
            ViewModel = App.Current.Services.GetService(typeof(ChatViewModel)) as ChatViewModel;
            DataContext = ViewModel;
        }
    }
}

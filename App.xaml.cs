using ChatWithMeWindows.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatWithMeWindows
{
    public partial class App : Application
    {
        private Window _window;

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _window = new MainWindow();

            //TODO

            var httpService = new HttpService("https://your-api-url.com/api");
            var webSocketService = new WebSocketService("wss://your-api-url.com/ws");
            var storageService = new StorageService();
            var navigationService = new NavigationService(_window as MainWindow);

            // 检查是否已登录
            var user = storageService.GetUser();

            // 检查是否已登录
            var user = storageService.GetUser();

            if (user != null)
            {
                // 已登录，导航到主页
                navigationService.NavigateTo("MainPage");
            }
            else
            {
                // 未登录，导航到登录页
                navigationService.NavigateTo("LoginPage");
            }

            _window.Activate();
        }

    }
}

using ChatWithMeWindows.Services;
using ChatWithMeWindows.ViewModels;
using ChatWithMeWindows.Views;
using Microsoft.Extensions.DependencyInjection;
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
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace ChatWithMeWindows
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            this.InitializeComponent();
            Services = ConfigureServices();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //register httpclient
            services.AddSingleton(new HttpClient());

            //register service layer
            services.AddSingleton<AuthenticationService>();
            services.AddSingleton<MessagingService>();

            //register viewmodel
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<ChatViewModel>();

            //register page (optional?
            services.AddSingleton<LoginPage>();
            services.AddSingleton<ChatPage>();

            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
        private Window m_window;
    }
}

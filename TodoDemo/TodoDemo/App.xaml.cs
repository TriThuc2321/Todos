using System;
using TodoDemo.MVVM.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new SplashScreenView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

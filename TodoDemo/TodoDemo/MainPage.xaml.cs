using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDemo.Database;
using TodoDemo.MVVM.View;
using Xamarin.Forms;

namespace TodoDemo
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
           
            Routing.RegisterRoute(nameof(SplashScreenView), typeof(SplashScreenView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(TodoView), typeof(TodoView));
            Routing.RegisterRoute(nameof(AboutView), typeof(AboutView));

            this.BindingContext = DataManager.Ins;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }
    }
}

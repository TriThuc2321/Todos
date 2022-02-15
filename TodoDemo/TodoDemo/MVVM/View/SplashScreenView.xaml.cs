using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDemo.Database;
using TodoDemo.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoDemo.MVVM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenView : ContentPage
    {
        public SplashScreenView()
        {
            InitializeComponent();
            this.BindingContext = new SplashScreenViewModel(Navigation);
            DataManager.Ins.currentShell = Shell.Current;
            DataManager.Ins.navigation = Navigation;
        }
    }
}
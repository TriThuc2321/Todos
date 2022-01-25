using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TodoDemo.MVVM.Model;
using TodoDemo.MVVM.View;
using Xamarin.Forms;

namespace TodoDemo.MVVM.ViewModel
{
    public class SplashScreenViewModel
    {
        public SplashScreenViewModel()
        {
        }

        INavigation navigation;

        public SplashScreenViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
        public ICommand StartCommand => new Command<object>(async (obj) =>
        {
            await navigation.PushAsync(new TodoView());
        });
    }
}

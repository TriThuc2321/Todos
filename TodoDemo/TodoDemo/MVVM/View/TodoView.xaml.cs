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
    public partial class TodoView : ContentPage
    {
        public TodoView()
        {
            InitializeComponent();            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new TodoViewModel(Navigation);
        }
    }
}
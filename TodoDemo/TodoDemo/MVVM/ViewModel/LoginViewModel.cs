using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TodoDemo.Core;
using TodoDemo.MVVM.View;
using Xamarin.Forms;

namespace TodoDemo.MVVM.ViewModel
{
    public class LoginViewModel: ObservableObject
    {
        public LoginViewModel() { }
        INavigation navigation;

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;

        }
        public ICommand LoginCommand => new Command<object>(async (obj) =>
        {
            /*Todo selectedTodo = obj as Todo;

            if (selectedTodo != null)
            {
                selectedTodo.Status = !selectedTodo.Status;
                findAndUpdate(Todos, selectedTodo);

                await DataManager.Ins.UserServices.UpdateUser(selectedTodo);
            }*/
        });

        public ICommand RegisterCommand => new Command<object>(async (obj) =>
        {
            await navigation.PushAsync(new RegisterView());
        });

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
    }
}

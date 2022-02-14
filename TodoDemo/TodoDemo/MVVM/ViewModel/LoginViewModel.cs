using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TodoDemo.Core;
using TodoDemo.Database;
using TodoDemo.MVVM.Model;
using TodoDemo.MVVM.View;
using Xamarin.Forms;

namespace TodoDemo.MVVM.ViewModel
{
    public class LoginViewModel: ObservableObject
    {
        public LoginViewModel() { }
        INavigation navigation;
        public Command EyeCommand { get; }

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            EyeSource = "eyeOffIcon.png";
            IsPassword = true;
            EyeCommand = new Command(eyeHandle);
        }
        public ICommand LoginCommand => new Command<object>(async (obj) =>
        {
            if (checkInfor())
            {
                User user = DataManager.Ins.ListUsers.Find(e => e.UserName == UserName);
                if (user == null)
                {
                    DependencyService.Get<IToast>().ShortToast("Tài khoản không tồn tại!");                    
                }
                else
                {
                    if(user.Password == Password)
                    {
                        DataManager.Ins.CurrentUser = user;
                        DependencyService.Get<IToast>().ShortToast("Đăng nhập thành công!");
                        await navigation.PushAsync(new TodoView());
                    }
                    else
                    {
                        DependencyService.Get<IToast>().ShortToast("Mật khẩu không đúng!");
                    }
                }
            }
            
        });

        bool checkInfor()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                DependencyService.Get<IToast>().ShortToast("Vui lòng nhập đầy đủ thông tin");
                return false;
            }
            else if (Password.Length < 6)
            {
                DependencyService.Get<IToast>().ShortToast("Mật khẩu dài hơn 5 kí tự");
                return false;
            }            
            return true;
        }

        void eyeHandle(object obj)
        {
            IsPassword = !IsPassword;
            EyeSource = !IsPassword ? "eyeIcon.png" : "eyeOffIcon.png";
        }
        
        private string _eyeSource;
        public string EyeSource
        {
            get { return _eyeSource; }
            set
            {
                _eyeSource = value;
                OnPropertyChanged("EyeSource");
            }
        }
        private bool _isPassword;
        public bool IsPassword
        {
            get { return _isPassword; }
            set
            {
                _isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }

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

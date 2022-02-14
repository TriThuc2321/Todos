using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TodoDemo.Core;
using TodoDemo.Database;
using TodoDemo.MVVM.Model;
using TodoDemo.MVVM.View;
using Xamarin.Forms;

namespace TodoDemo.MVVM.ViewModel
{
    public class RegisterViewModel: ObservableObject
    {
        public RegisterViewModel()
        {
        }

        public Command EyeCommand { get; }
        public Command EyeConfirmCommand { get; }
        INavigation navigation;

        public RegisterViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            EyeSource = "eyeOffIcon.png";
            IsPassword = true;
            EyeCommand = new Command(eyeHandle);

            EyeSourceConfirm = "eyeOffIcon.png";
            IsConfirmPassword = true;
            EyeConfirmCommand = new Command(eyeConfirmHandle);
        }
        public ICommand LoginCommand => new Command<object>(async (obj) =>
        {
            await navigation.PopAsync();
        });
        public ICommand RegisterCommand => new Command<object>(async (obj) =>
        {
            if (checkInfor())
            {
                await addUser();
            }
            
        });

        private async Task addUser()
        {
            User user = new User(DataManager.Ins.UserServices.GenerateId(), UserName, Password, null);

            DataManager.Ins.ListUsers.Add(user);
            await DataManager.Ins.UserServices.AddUser(user);
            DependencyService.Get<IToast>().ShortToast("Đăng ký tài khoản thành công!");
            await navigation.PopAsync();
        }

        bool checkInfor()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                DependencyService.Get<IToast>().ShortToast("Vui lòng nhập đầy đủ thông tin");
                return false;
            }           
            else if (Password.Length < 6)
            {
                DependencyService.Get<IToast>().ShortToast("Mật khẩu dài hơn 5 kí tự");
                return false;
            }
            else if (Password != ConfirmPassword)
            {
                DependencyService.Get<IToast>().ShortToast("Xác nhận mật khẩu không trùng khớp");
                return false;
            }
            else if (DataManager.Ins.UserServices.userExist(UserName, DataManager.Ins.ListUsers))
            {
                DependencyService.Get<IToast>().ShortToast("Tài khoản đã được sử dụng");
                return false;
            }
            return true;
        }
        void eyeHandle(object obj)
        {
            IsPassword = !IsPassword;
            EyeSource = !IsPassword ? "eyeIcon.png" : "eyeOffIcon.png";
        }
        void eyeConfirmHandle(object obj)
        {
            IsConfirmPassword = !IsConfirmPassword;
            EyeSourceConfirm = !IsConfirmPassword ? "eyeIcon.png" : "eyeOffIcon.png";
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
        private string _eyeSourceConfirm;
        public string EyeSourceConfirm
        {
            get { return _eyeSourceConfirm; }
            set
            {
                _eyeSourceConfirm = value;
                OnPropertyChanged("EyeSourceConfirm");
            }
        }
        private bool _isConfirmPassword;
        public bool IsConfirmPassword
        {
            get { return _isConfirmPassword; }
            set
            {
                _isConfirmPassword = value;
                OnPropertyChanged("IsConfirmPassword");
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

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
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
    }
}

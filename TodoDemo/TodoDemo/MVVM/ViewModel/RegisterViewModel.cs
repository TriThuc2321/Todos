using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TodoDemo.Core;
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
            
            
        });
        /*bool checkInfor()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                DependencyService.Get<IToast>().ShortToast("Vui lòng nhập đầy đủ thông tin");
            }
            else if (!DataManager.Ins.UsersServices.checkEmail(Account))
            {
                DependencyService.Get<IToast>().ShortToast("Email không đúng định dạng");
            }
            else if (Password.Length < 6)
            {
                DependencyService.Get<IToast>().ShortToast("Mật khẩu dài hơn 5 kí tự");
            }
            else if (Password != ConfirmPassword)
            {
                DependencyService.Get<IToast>().ShortToast("Xác nhận mật khẩu không trùng khớp");
            }
            else if (DataManager.Ins.UsersServices.ExistEmail(Account, DataManager.Ins.users))
            {
                DependencyService.Get<IToast>().ShortToast("Email đã được sử dụng");
            }
            else
            {
                Random rand = new Random();
                string randomCode = (rand.Next(999999)).ToString();
                DataManager.Ins.VerifyCode = randomCode;

                DataManager.Ins.CurrentUser = new User()
                {
                    email = Account,
                    password = DataManager.Ins.UsersServices.Encode(Password),
                    name = Name,
                    contact = "",
                    birthday = "",
                    cmnd = "",
                    profilePic = "defaultUser.png",
                    address = "",
                    score = 0,
                    rank = 3
                };

                await SendEmail("Mã xác nhận", "Cảm ơn bạn đã sử dụng LikeFly, đây là mã xác nhận của bạn: " + randomCode, Account);
                DependencyService.Get<IToast>().ShortToast("Mã xác nhận đã được gửi đến email của bạn");
                DataManager.Ins.users.Add(DataManager.Ins.CurrentUser);
                DataManager.Ins.ListUsers.Add(DataManager.Ins.CurrentUser);
                //navigation.PushAsync(new ConfirmEmailView());
                await currentShell.GoToAsync($"{nameof(ConfirmEmailView)}");
            }
            return true;
        }*/
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

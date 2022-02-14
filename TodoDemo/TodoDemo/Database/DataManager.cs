using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TodoDemo.Core;
using TodoDemo.MVVM.Model;

namespace TodoDemo.Database
{
    public class DataManager: ObservableObject
    {
        private static DataManager _ins;
        public static DataManager Ins 
        {
            get
            {
                if (_ins == null) _ins = new DataManager();
                return _ins;
            }
            set { _ins = value; }
        }
        public bool loadData;

        public List<User> ListUsers;
        public UserServices UserServices;
        public DataManager()
        {
            UserServices = new UserServices();

            getData();
            
        }

        async Task getData()
        {
            ListUsers = await UserServices.GetAllUsers();            
        }      
        
        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set {
                _currentUser = value;
                ProfilePic = "avatar.png";
                OnPropertyChanged("CurrentUser");            
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

        private string _profilePic;
        public string ProfilePic
        {
            get { return _profilePic; }
            set
            {
                _profilePic = value;
                OnPropertyChanged("ProfilePic");
            }
        }
    }
}

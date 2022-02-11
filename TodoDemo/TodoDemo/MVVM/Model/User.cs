using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TodoDemo.Core;

namespace TodoDemo.MVVM.Model
{
    public class User : ObservableObject
    {
        public User() { }

        public User(string id, string userName, string password, ObservableCollection<Todo> todos)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Todos = todos;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                _username = value;
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

        private ObservableCollection<Todo> _todos;
        public ObservableCollection<Todo> Todos
        {
            get { return _todos; }
            set { 
                _todos = value; 
                OnPropertyChanged("Todos"); 
            }
        }
    }
}

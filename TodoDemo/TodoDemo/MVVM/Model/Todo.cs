using System;
using System.Collections.Generic;
using System.Text;
using TodoDemo.Core;

namespace TodoDemo.MVVM.Model
{
    public class Todo: ObservableObject
    {
        public Todo() { }

        public Todo(string id, string name, bool status)
        {
            Id = id;
            Name = name;
            Status = status;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { 
                _id = value;
            OnPropertyChanged("Id");
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

        private bool _status;
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

    }
}

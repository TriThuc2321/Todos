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

        public List<Todo> ListTodos;
        public TodoServices TodoServices;
        public DataManager()
        {
            TodoServices = new TodoServices();

            getData();
        }

        async Task getData()
        {
            ListTodos = await TodoServices.GetAllTodos();            
        }      
        
    }
}

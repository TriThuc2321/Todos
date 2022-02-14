using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public class TodoViewModel: ObservableObject
    {
        public TodoViewModel()
        {
        }

        INavigation navigation;

        public TodoViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            getData();
        }

        async Task getData()
        {
            Todos = new ObservableCollection<Todo>();
            foreach (Todo todo in DataManager.Ins.CurrentUser.Todos)
            {
                Todos.Add(todo);
            }

        }
        public ICommand BackNavigationCommand => new Command<object>(async (obj) =>
        {
            await navigation.PopAsync();

        });
        public ICommand NewTodoCommand => new Command<object>(async (obj) =>
        {
            if (string.IsNullOrEmpty(TaskInput))
            {
                DependencyService.Get<IToast>().ShortToast("Please enter your task");
                return;
            }
            else
            {
                string id = DataManager.Ins.UserServices.GenerateId();
                CultureInfo viVn = new CultureInfo("vi-VN");
                string date = DateTime.Now.ToString("d", viVn);

                Todo newTodo = new Todo(id, TaskInput, false, date);

                Todos.Add(newTodo);
                if(DataManager.Ins.CurrentUser.Todos == null)
                {
                    DataManager.Ins.CurrentUser.Todos = new List<Todo>();
                }
                DataManager.Ins.CurrentUser.Todos.Add(newTodo);

                await DataManager.Ins.UserServices.UpdateUser(DataManager.Ins.CurrentUser);

                TaskInput = "";
            }
            
        });
        public ICommand DeleteCommand => new Command<object>(async (obj) =>
        {
            Todo selectedTodo = obj as Todo;

            if (selectedTodo != null)
            {
                Todos.Remove(selectedTodo);
                DataManager.Ins.CurrentUser.Todos.Remove(selectedTodo);
                await DataManager.Ins.UserServices.UpdateUser(DataManager.Ins.CurrentUser);
            }
        });

        public ICommand CheckCommand => new Command<object>(async (obj) =>
        {
            Todo selectedTodo = obj as Todo;
            selectedTodo.Status = !selectedTodo.Status;

            if (selectedTodo != null)
            {
                for (int i = 0; i < DataManager.Ins.CurrentUser.Todos.Count; i++)
                {
                    if (DataManager.Ins.CurrentUser.Todos[i].Id == selectedTodo.Id)
                    {
                        DataManager.Ins.CurrentUser.Todos[i] = selectedTodo;
                        Todos[i] = selectedTodo;
                    }
                }
            }
        });

        bool findAndUpdate(ObservableCollection<Todo> mList, Todo value)
        {
            for(int i =0; i< mList.Count; i++)
            {
               if( mList[i].Id == value.Id)
                {
                    mList[i] = value;
                    return true;
                }
            }
            return false;
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
        private Todo _selectedTodo;
        public Todo SelectedTodo
        {
            get
            {
                return _selectedTodo;
            }
            set
            {
                _selectedTodo = value;
                OnPropertyChanged("SelectedTodo");
            }
        }
        private string _taskInput;
        public string TaskInput
        {
            get
            {
                return _taskInput;
            }
            set
            {
                _taskInput = value;
                OnPropertyChanged("TaskInput");
            }
        }
    }
}

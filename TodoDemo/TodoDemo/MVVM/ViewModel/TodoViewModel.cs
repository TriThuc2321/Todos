﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            foreach (Todo todo in DataManager.Ins.ListTodos)
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
            if(TaskInput != null || TaskInput != "")
            {
                string id = DataManager.Ins.TodoServices.GenerateId();
                Todo newTodo = new Todo(id, TaskInput, false);

                Todos.Add(newTodo);
                DataManager.Ins.ListTodos.Add(newTodo);
                await DataManager.Ins.TodoServices.AddTodo(newTodo);

                TaskInput = "";
            }
            
        });
        public ICommand DeleteCommand => new Command<object>(async (obj) =>
        {
            Todo selectedTodo = obj as Todo;

            if (selectedTodo != null)
            {
                Todos.Remove(selectedTodo);
                DataManager.Ins.ListTodos.Remove(selectedTodo);
                await DataManager.Ins.TodoServices.DeleteTodo(selectedTodo);
            }
        });

        public ICommand CheckCommand => new Command<object>(async (obj) =>
        {
            Todo selectedTodo = obj as Todo;

            if (selectedTodo != null)
            {
                selectedTodo.Status = !selectedTodo.Status;
                findAndUpdate(Todos, selectedTodo);

                await DataManager.Ins.TodoServices.UpdateTodo(selectedTodo);
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

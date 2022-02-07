using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoDemo.MVVM.Model;

namespace TodoDemo.Database
{
    public class TodoServices
    {
        FirebaseClient firebase = new FirebaseClient("https://zendi-1e684-default-rtdb.firebaseio.com/");        
        public TodoServices()
        {
        }
        public async Task<List<Todo>> GetAllTodos()
        {
            return (await firebase
              .Child("Todos")
              .OnceAsync<Todo>()).Select(item => new Todo
              {
                  Id = item.Object.Id,
                  Name = item.Object.Name,
                  Status = item.Object.Status,
                  Time = item.Object.Time,  
              }).ToList();
        }
        public async Task AddTodo(Todo todo)
        {
            await firebase
              .Child("Todos")
              .PostAsync(new Todo()
              {
                  Id = todo.Id,
                  Name = todo.Name,
                  Status = todo.Status,
                  Time= todo.Time,
              });
        }
        public async Task UpdateTodo(Todo todo)
        {
            var toUpdateTodo = (await firebase
              .Child("Todos")
              .OnceAsync<Todo>()).Where(a => a.Object.Id == todo.Id).FirstOrDefault();

            await firebase
              .Child("Todos")
              .Child(toUpdateTodo.Key)
              .PutAsync(new Todo
              {
                  Id = todo.Id,
                  Name = todo.Name,
                  Status = todo.Status,
                  Time = todo.Time
              });
        }

        public string GenerateId(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        public async Task DeleteTodo(Todo todo)
        {
            var toDeleted = (await firebase
               .Child("Todos").OnceAsync<Todo>()).FirstOrDefault(p => p.Object.Id == todo.Id);

            await firebase.Child("Todos").Child(toDeleted.Key).DeleteAsync();

        }
    }
}

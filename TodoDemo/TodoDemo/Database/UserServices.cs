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
    public class UserServices
    {
        FirebaseClient firebase = new FirebaseClient("https://zendi-1e684-default-rtdb.firebaseio.com/");
        public UserServices()
        {

        }
        public async Task<List<User>> GetAllUsers()
        {
            return (await firebase
              .Child("UsersTodo")
              .OnceAsync<User>()).Select(item => new User
              {
                  Id = item.Object.Id,
                  UserName = item.Object.UserName,
                  Password = item.Object.Password,
                  Todos = item.Object.Todos,
              }).ToList();
        }
        public User GetUser(List<User> listUsers, string userId)
        {
            return listUsers.Where(a => a.Id == userId).FirstOrDefault();
        }
        public async Task AddUser(User user)
        {
            await firebase
              .Child("UsersTodo")
              .PostAsync(new User()
              {
                  Id = user.Id,
                  UserName = user.UserName,
                  Password = user.Password,
                  Todos = user.Todos,
              });
        }
        public async Task UpdateUser(User user)
        {
            var toUpdateUser = (await firebase
              .Child("UsersTodo")
              .OnceAsync<User>()).Where(a => a.Object.Id == user.Id).FirstOrDefault();

            await firebase
              .Child("UsersTodo")
              .Child(toUpdateUser.Key)
              .PutAsync(new User
              {
                  Id = user.Id,
                  UserName = user.UserName,
                  Password = user.Password,
                  Todos = user.Todos,
              });
        }
        public async Task DeleteUser(string userId)
        {
            var toDeleted = (await firebase
               .Child("UsersTodo").OnceAsync<User>()).FirstOrDefault(p => p.Object.Id == userId);

            await firebase.Child("UsersTodo").Child(toDeleted.Key).DeleteAsync();

        }
        public string GenerateId(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        public bool userExist(string userName, List<User> listUsers)
        {
            return listUsers.Find(e=> e.UserName == userName) != null;
        }
    }
}

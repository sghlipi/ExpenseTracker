using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Cryptography;
using Expense_Tracker.Services;
using Expense_Tracker.Model;

namespace Expense_Tracker.Services

{
    public class UserService : IUserService
    {
        
        private readonly string usersFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
            "UserInfo.json");


        //Validate User Details and adds the detail to 'usersFilePath' JSON File
        public async Task<bool> UserRegister(User newUser)
        {
            // try
            // {
                if (await UserExists(newUser.Name)) //To check if user alreadu exist.
                {
                    return false;
                }

                var users = await LoadUsersAsync();
                users.Add(newUser);
                await SaveUsers(users);
                return true;
                

        }


        public async Task<User> GetUser(string Name)
        {
                var users = await LoadUsersAsync();
                return users.FirstOrDefault(n => n.Name == Name);
        }

        public async Task<bool> UserExists(string Name) //check is user exit by name
        {
            var users = await LoadUsersAsync();
            return users.Any(user => user.Name == Name);
        }

        private async Task<List<User>> LoadUsersAsync() //Load user from json file
        {
            if (!File.Exists(usersFilePath))
            {
                return new List<User>();
            }

            var json = await File.ReadAllTextAsync(usersFilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();

        }

        private async Task SaveUsers(List<User> users) //Save user to JSON File
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(usersFilePath, json);
            Console.WriteLine($"Save user:");

        }

    }
}

       
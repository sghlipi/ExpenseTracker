using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expense_Tracker.Model;

namespace Expense_Tracker.Services
{
    public interface IUserService
    {
        Task<bool> UserRegister(User newUser);

        Task<bool> UserExists(string Name);
        
        Task<User> GetUser(string Name);
        
    }
}
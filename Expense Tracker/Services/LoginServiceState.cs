using System;
using Expense_Tracker.Model;

namespace Expense_Tracker.Services
{
    public class LoginStateService
    {
        private User currentUser; //retrives the authenticated user

        public User GetAuthenticatedUser()
        {
            return currentUser;
        }

        public void SetAuthenticatedUser(User user)
        {
            currentUser = user;
        }

        public bool AuthenticatedUser(string Name, string password)
        {
            if (currentUser!=null)
            {
                return true;
            }
            return false;
        }

        public void Logout()
        {
            currentUser = null;
        }
    }
}
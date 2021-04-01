using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Users
{
    public class User : Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public User( string username, string password)
        {
            Username = username;
            Password = password;
        }

        // Cannot bind paramater fix
        protected User() {}

        public (List<string> errors, bool isValid) Validate()
        {
            var errs = new List<string>(); 
            
            if (!ValidateUsername())
            {
                errs.Add("Invalid username");
            }
            
            if (!ValidatePassword())
            {
                errs.Add("Invalid password");
            }

            return (errs, errs.Count == 0);
        }
        
        private bool ValidateUsername()
        {
            if (String.IsNullOrWhiteSpace(Username))
            {
                return false;
            }

            return true;
        }

        private bool ValidatePassword()
        {
            if (String.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return true;
        }
        
    }
}

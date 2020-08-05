using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TibaExam.Interfaces;
using TibaExam.Models;

namespace TibaExam.Services
{
    public class UserService : IUserService
    {
        private IConfiguration config;

        public UserService(IConfiguration cfg)
        {
            config = cfg;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            { 
                return null; 
            }

            // Currently, user is stored in config, as the task didn't ask for 
            // any user management besides login...
            var user = new User
            {
                Id = config.GetValue<int>("LoginCredentials:Id"),
                Username = config.GetValue<string>("LoginCredentials:Username"),
                Password = config.GetValue<string>("LoginCredentials:Password")
            };

            // Check if credentials match
            if (user == null || username != user.Username || password != user.Password)
            { 
                return null; 
            }

            // Authentication successful
            return user;
        }
    }
}
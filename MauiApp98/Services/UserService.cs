using MauiApp98.Data;
using MauiApp98.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;
using BCrypt.Net;
using MauiApp98.Models;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MauiApp98.Services
{

    public class UserService
    {
        private readonly SqliteData database;
        private readonly UserRepository userRepository;
        public UserService(SqliteData database)
        {
            this.database = database;
            userRepository = new UserRepository(database);
        }
        public Users? AuthenticateUser(string username, string password)
        {
            var user = userRepository.GetUserbyUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) 
            {
                Debug.WriteLine(user.Username + " LOGGED");
                return user; 
            }
            else return null;
        }

        public bool RegisterUser(string username, string email, string password, string repeatpassword) 
        {
            //Regex regex = new Regex(@"^.+@.+\..+$");
            if (userRepository.GetUserbyUsername(username)==null && password.Length > 7 && password == repeatpassword)// && regex.IsMatch(email))
            {
                Users user = new Users();
                user.Username = username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                user.Email = email;
                database.Insert(user);
                Debug.WriteLine(user.Username + "registered");
                return true;
            }
            return false;
        }

        public Users? GetUserbyUsername(string username)
        {
            return database.GetAll<Users>().FirstOrDefault(user => user.Username == username);
        }
        
        public List<Users> getAllUsers()
        {
            return database.GetAll<Users>();

        }
        
        public void DeleteUser(Users user)
        {
            database.Update(user);
        }
        
    }
}

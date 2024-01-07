using MauiApp98.Data;
using MauiApp98.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp98.Repositories
{
    public class UserRepository
    {
        private readonly SqliteData database;

        public UserRepository(SqliteData database)
        {
            this.database = database;
        }

        public Users? GetUserbyUsername(string username)
        {
            return database.GetAll<Users>().FirstOrDefault(user => user.Username == username);
        }
    }
}

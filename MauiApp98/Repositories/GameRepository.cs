using MauiApp98.Data;
using MauiApp98.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp98.Repositories
{
    public class GameRepository
    {

        private readonly SqliteData database;

        public GameRepository(SqliteData database)
        {
            this.database = database;
        }

        public Games? getGameById(int id)
        {
            return database.GetAll<Games>().FirstOrDefault(game => game.Id == id);
        }

 


    }
}

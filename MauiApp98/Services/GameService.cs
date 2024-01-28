using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp98.Services
{
    internal class GameService
    {
        private readonly SqliteData database;
        private readonly GameRepository gamerepository;
        public GameService(SqliteData database)
        {
            this.database = database;
            gamerepository = new GameRepository(database);
        }

        public List<Games> getAllGames()
        {
            return database.GetAll<Games>();

        }
        public void AddGame(Games game)
        {
            database.Insert(game);
        }

        public void DeleteGame(Games game)
        {
            database.Delete(game);
        }

        public void UpdateGame(Games game)
        {
            database.Update(game);
        }
    }
    
}

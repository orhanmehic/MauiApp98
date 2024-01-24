using MauiApp98.Data;
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
    }
}

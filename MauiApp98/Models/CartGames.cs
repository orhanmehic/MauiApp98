using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp98.Models
{
    internal class CartGames
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CartId { get; set; }
        public int GameId { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp98.Models
{
    public class Games
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public double Price { get; set; }
        public string Description { get; set; }
        public string Logo {  get; set; }

        public string Category {  get; set; }
      
    }
}

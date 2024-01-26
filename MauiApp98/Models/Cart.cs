using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace MauiApp98.Models
{
    public class Cart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, Unique]

        public Users User { get; set; }
       
        public List<Games> Games { get; set; }
    }
}

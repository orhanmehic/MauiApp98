using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MauiApp98.Models
{
    public class Cart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
       

        public int UserId { get; set; }

        public int GameId {  get; set; }
   
       
    }
}

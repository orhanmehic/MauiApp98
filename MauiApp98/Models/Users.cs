using SQLite;
using System.ComponentModel;

namespace MauiApp98.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, Unique]
        public string Username { get; set; }
        [NotNull]
        public string Password { get; set; }
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        public string Role { get; set; } = "user";

    }
}
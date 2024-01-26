using MauiApp98.Models;
using SQLite;

namespace MauiApp98.Data;

public class SqliteData
{
    private readonly SQLiteConnection database;

    public SqliteData(string dbPath)
    {
        database = new SQLiteConnection(dbPath);
        InitializeDatabase();
    }
    public List<T> GetAll<T>() where T : new()
    {
        return database.Table<T>().ToList();
    }

    public void Insert<T>(T item)
    {
        database.Insert(item);
    }

    public void Update<T>(T item) {  
        database.Update(item); 
    }

    public void Delete<T>(T item) {  
        database.Delete(item);
    }
    private void InitializeDatabase()
    {
        database.CreateTable<Users>();
        database.CreateTable<Games>();
        database.CreateTable<Cart>();
    }

}
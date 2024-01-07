//using MauiApp98.Data;
using SQLite;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using MauiApp98.Views;
using MauiApp98.Data;
using MauiApp98.Services;

namespace MauiApp98;

public partial class MainPage : ContentPage
{


    private UserService userService;

    public MainPage()
    {
        InitializeComponent();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
    }

    private void Login(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login(userService));
    }

    private void Registration(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Register(userService));
    }
}
    

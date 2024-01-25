//using MauiApp98.Data;
using SQLite;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using MauiApp98.Views;
using MauiApp98.Data;
using MauiApp98.Services;
using MauiApp98.Models;
using System.Collections.ObjectModel;

namespace MauiApp98;

public partial class MainPage : ContentPage
{


    private UserService userService;
    private GameService gameService;

    public ObservableCollection<Games> Games { get; set; }

    public MainPage()
    {
        InitializeComponent();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
        gameService = new GameService(database);
        Games = new ObservableCollection<Games>(gameService.getAllGames());
        BindingContext = this;
        isLoggedIn();
    }

    public async void isLoggedIn()
    {

        if (!String.IsNullOrEmpty(await SecureStorage.GetAsync("username")))
        {
            LoginButton.IsVisible = false;
            LogoutButton.IsVisible = true;
        }
        else 
        { 
            LoginButton.IsVisible= true;
            LogoutButton.IsVisible = false;
        }
    }

    private void Login(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login(userService));
    }

    private void Registration(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Register(userService));
    }

    private void Logout(object sender, EventArgs e)
    {
        SecureStorage.Remove("username");
        Navigation.PopToRootAsync();
    }
    
}

    

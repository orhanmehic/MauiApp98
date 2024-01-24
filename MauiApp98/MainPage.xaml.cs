//using MauiApp98.Data;
using SQLite;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using MauiApp98.Views;
using MauiApp98.Data;
using MauiApp98.Services;
using Android.App;
using MauiApp98.Models;

namespace MauiApp98;

public partial class MainPage : ContentPage
{


    private UserService userService;
    private GameService gameService;

    public MainPage()
    {
        InitializeComponent();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
        gameService = new GameService(database);
        loadGames();


    }

    private void Login(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Login(userService));
    }

    private void Registration(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Register(userService));
    }

    private void loadGames()
    {
        List<Games> games = gameService.getAllGames();

      
        foreach (var game in games)
        {
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Padding = 10
            };

            Frame frame = new Frame
            {
                CornerRadius = 10,
            };

            Label titleLabel = new Label
            {
                Text = game.Name,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold
            };

            

            frame.Content = stackLayout;
            stackLayout.Children.Add(titleLabel);
     
            GamesStackLayout.Children.Add(frame);
        }
    }
}

    

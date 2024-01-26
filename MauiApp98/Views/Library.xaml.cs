using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;
using System.Collections.ObjectModel;

namespace MauiApp98.Views;

public partial class Library : ContentPage
{

    private readonly CartService cartService;
    private readonly UserService userService;
    private readonly GameService gameService;

    public ObservableCollection<Games> GamesInCart { get; set; }

    public Library()
    {
        InitializeComponent();

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
        gameService = new GameService(database);

    

        GamesInCart = new ObservableCollection<Games>();
        BindingContext = this;

        LoadGamesInCart();
    }

    private void LoadGamesInCart()
    {
        // Get the username of the logged-in user
        var username = SecureStorage.GetAsync("username").Result;

        // Get the userId associated with the logged-in username
        var userId = userService.GetUserbyUsername(username).Id;

        // Check if the userId is valid
        if (userId > 0)
        {
            // Load games in the user's cart
            var gamesInCart = cartService.GetGamesInCart(userId);
            GamesInCart.Clear();

            foreach (var game in gamesInCart)
            {
                GamesInCart.Add(game);
            }
        }
        else
        {
            // Handle the case where the userId is not valid
        }
    }


}
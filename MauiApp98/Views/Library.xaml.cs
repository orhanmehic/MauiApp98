using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp98.Views;

public partial class Library : ContentPage, INotifyPropertyChanged
{

    private readonly CartService cartService;
    private readonly UserService userService;
    private readonly GameService gameService;

    public ObservableCollection<Games> GamesInCart { get; set; }
  

    private double _totalPrice;
    public double TotalPrice
    {
        get { return _totalPrice; }
        set
        {
            if (_totalPrice != value)
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public Library()
    {
        InitializeComponent();

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
        gameService = new GameService(database);
        cartService = new CartService(database);

        GamesInCart = new ObservableCollection<Games>();
        BindingContext = this;

        LoadGamesInCart();
        UpdateTotalPrice();
    }

    private void ReturnToHomePage(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }

    private void LoadGamesInCart()
    {
        var username = SecureStorage.GetAsync("username").Result;
        int userId = userService.GetUserbyUsername(username).Id;

        if (userId > 0)
        {
            var gamesInCart = cartService.GetGamesInCart(userId);
            GamesInCart.Clear();

            foreach (var game in gamesInCart)
            {
                GamesInCart.Add(game);
            }

            UpdateTotalPrice(); // Refresh the total price after loading games
        }
        else
        {
            // Handle the case where the userId is not valid
        }
    }

    private void UpdateTotalPrice()
    {
        // Calculate the total price based on the games in the cart
        TotalPrice = GamesInCart.Sum(game => game.Price);
    }

    private void RemoveFromCartButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button removeFromCartButton && removeFromCartButton.CommandParameter is Games selectedGame)
        {
            var username = SecureStorage.GetAsync("username").Result;
            int userId = userService.GetUserbyUsername(username).Id;

            if (userId > 0)
            {
                cartService.RemoveGameFromCart(userId, selectedGame);
                LoadGamesInCart(); // Refresh the displayed games and total price after removing from cart
            }
            else
            {
                // Handle the case where the userId is not valid
            }
        }
    }
    private void BuyAllButton_Clicked(object sender, EventArgs e)
    {
        var username = SecureStorage.GetAsync("username").Result;
        int userId = userService.GetUserbyUsername(username).Id;

        if (userId > 0)
        {
            // Implement the logic to complete the purchase (empty the cart)
            cartService.EmptyCart(userId);
            LoadGamesInCart(); // Refresh the displayed games after buying all
            UpdateTotalPrice(); // Refresh the displayed total price after buying all
        }
        else
        {
            // Handle the case where the userId is not valid
        }
    }

}
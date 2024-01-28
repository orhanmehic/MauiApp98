﻿using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;
using MauiApp98.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace MauiApp98;

public partial class MainPage : ContentPage
{


    private UserService userService;
    private GameService gameService;
    private CartService cartService;

    public ObservableCollection<Games> Games { get; set; }

    public MainPage()
    {
        InitializeComponent();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
        gameService = new GameService(database);
        cartService = new CartService(database);
        Games = new ObservableCollection<Games>(gameService.getAllGames());
        BindingContext = this;
        isLoggedIn();
       

    }

    public void adminPage(Object sender, EventArgs e)
    {
        AdminPage adminPage = new AdminPage();
        Navigation.PushAsync(adminPage);
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
            LoginButton.IsVisible = true;
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
    private async void GameTapped(object sender, EventArgs e)
    {
        if (sender is StackLayout stackLayout && stackLayout.BindingContext is Games game)
        {
            // Navigate to the GameAboutPage and pass the selected game as a parameter
            await Navigation.PushAsync(new aboutGame(game, this));
        }
    }



    public void AddToCart(Games game)
    {

            if (!String.IsNullOrEmpty(SecureStorage.GetAsync("username").Result))
            {
                // Get the username of the logged-in user
                var username = SecureStorage.GetAsync("username").Result;

                // Get the user associated with the logged-in username
                var user = userService.GetUserbyUsername(username);

                Debug.WriteLine(username);

                // Check if the user object is not null
                if (user != null)
                {
                    // Get the userId from the user object
                    var userId = user.Id;

                    // Check if the userId is valid
                    if (userId > 0)
                    {
                        // Add the selected game to the user's cart
                        cartService.AddGameToCart(userId, game);
                    }
                    else
                    {
                        // Handle the case where the userId is not valid
                    }
                }
                else
                {
                    // Handle the case where the user object is null
                }
            }
            else
            {
                // Handle the case where the user is not logged in
            }
        

    }

    public void ClickedLibrary(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Library());

    }

  

}
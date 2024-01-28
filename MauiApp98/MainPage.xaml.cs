using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;
using MauiApp98.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp98
{
    public partial class MainPage : ContentPage
    {
        private UserService userService;
        private GameService gameService;
        private CartService cartService;
        public ObservableCollection<Games> Games { get; set; }
        private string searchText;

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

            isLoggedIn();

        }

        private void Registration(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register(userService));
        }

        private void Logout(object sender, EventArgs e)
        {
            SecureStorage.Remove("username");


        }
    
    private async void GameTapped(object sender, EventArgs e)
    {
        if (sender is StackLayout stackLayout && stackLayout.BindingContext is Games game)
        {
            // Navigate to the GameAboutPage and pass the selected game as a parameter
            await Navigation.PushAsync(new aboutGame(game, this));
        }
    }

        private void Library(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Library());
        }

        private void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button addToCartButton && addToCartButton.CommandParameter is Games selectedGame)
            {
                Debug.WriteLine($"Selected Game: {selectedGame.Name}");
                AddToCart(selectedGame);
            }
        }

        public void AddToCart(Games game)
        {
            if (!String.IsNullOrEmpty(SecureStorage.GetAsync("username").Result))
            {
                var username = SecureStorage.GetAsync("username").Result;
                var user = userService.GetUserbyUsername(username);

                    Debug.WriteLine(username);

                    if (user != null)
                    {
                        var userId = user.Id;

                        if (userId > 0)
                        {
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

        private void ViewDetailsButton(object sender, EventArgs e)
        {
            if (sender is Button ViewDetailsButton && ViewDetailsButton.CommandParameter is Games selectedGame)
            {
                Debug.WriteLine($"Selected Game: {selectedGame.Name}");

                Games SelectedGame = selectedGame;
                gameDetailsFrame.IsVisible = true;
                gameDetailsImage.Source = SelectedGame.Logo;
                gameDetailsDescription.Text = SelectedGame.Description;
            }
        }

        private void CloseGameDetails(object sender, EventArgs e)
        {
            gameDetailsFrame.IsVisible = false;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchText = e.NewTextValue;
            FilterGames();
        }

        private void FilterGames()
        {
            
            if (string.IsNullOrEmpty(searchText))
            {
                
                Games = new ObservableCollection<Games>(gameService.getAllGames());
            }
            else
            {
                // Filter games based on the search text
                Games = new ObservableCollection<Games>(gameService.getAllGames().Where(game => game.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
            }

            OnPropertyChanged(nameof(Games)); 
        }

    }    }


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;

namespace MauiApp98.Views
{
    public partial class AdminGames : TabbedPage
    {
        private GameService gameService;
        public ObservableCollection<Games> Games { get; set; }

        public List<string> Categories { get; set; }

        public AdminGames()
        {
            InitializeComponent();
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
            SqliteData database = new SqliteData(dbPath);
            gameService = new GameService(database);
            Games = new ObservableCollection<Games>(gameService.getAllGames());
            Categories = new List<string>()
            {
                "Action", "Adventure", "Sports", "Racing", "Horror","FPS", "RPG","Strategy", "Simulation" 
            };
            BindingContext = this;
            this.CurrentPageChanged += HandleCurrentPageChanged;
        }

        

        private void HandleCurrentPageChanged(object sender, EventArgs e)
        {
            // Check which tab is currently selected and navigate accordingly
            if (CurrentPage is ContentPage currentPage)
            {
                if (currentPage.Title == "Add Games")
                {
                    // Navigate to AddGamesPage
                    Navigation.PushAsync(new AdminAddGames());
                }
                else if (currentPage.Title == "Manage Games")
                {
                    // Navigate to ManageGamesPage
                    Navigation.PushAsync(new AdminManageGames());
                }
            }
        }

        private async void BackButton_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            // Get values entered by the user
            string name = NameEntry.Text;
            double price = double.Parse(PriceEntry.Text);
            string description = DescriptionEntry.Text;
            string logo = LogoEntry.Text;
            string category = CategoriesPicker.SelectedItem.ToString();


            // Create a new Games object
            Games newGame = new Games
            {
                Name = name,
                Price = price,
                Description = description,
                Logo = logo,
                Category = category
            };

            // Call the method to add the game to the database
            gameService.AddGame(newGame);

            // Clear input fields
            NameEntry.Text = "";
            PriceEntry.Text = "";
            DescriptionEntry.Text = "";
            LogoEntry.Text = "";

            // Display success message
            await DisplayAlert("Success", $"{name} has been successfully added.", "OK");

            // Optionally, update the UI to reflect the changes
            Games.Add(newGame);
        }
        
        private async void DeleteButton_Clicked(object? sender, EventArgs e)
        {
            // Retrieve the game that corresponds to the clicked button
            var game = (sender as Button)?.CommandParameter as Games;
    
            if (game != null)
            {
                // Display confirmation dialog before deleting
                bool answer = await DisplayAlert("Confirmation", $"Are you sure you want to delete {game.Name}?", "Yes", "No");

                if (answer)
                {
                    // Call the method to delete the game from the database
                    gameService.DeleteGame(game);
            
                    // Remove the game from the ObservableCollection
                    Games.Remove(game);

                    // Display success message
                    await DisplayAlert("Success", $"{game.Name} has been successfully deleted.", "OK");
                }
            }
        }

        private async void UpdateButton_Clicked(object? sender, EventArgs e)
        {
            // Retrieve the game that corresponds to the clicked button
            var game = (sender as Button)?.CommandParameter as Games;

            if (game != null)
            {
                // Display input dialogs to edit the game details
                string newName = await DisplayPromptAsync("Edit Name", "Enter new name:", initialValue: game.Name);
                string newPriceString = await DisplayPromptAsync("Edit Price", "Enter new price:", initialValue: game.Price.ToString());
                string newDescription = await DisplayPromptAsync("Edit Description", "Enter new description:", initialValue: game.Description);
                string newLogo = await DisplayPromptAsync("Edit Logo", "Enter new logo:", initialValue: game.Logo);

                if (newName != null && double.TryParse(newPriceString, out double newPrice))
                {
                    // Update the game details
                    game.Name = newName;
                    game.Price = newPrice;
                    game.Description = newDescription;
                    game.Logo = newLogo;

                    // Call the method to update the game in the database
                    gameService.UpdateGame(game);

                    // Find the index of the updated game in the collection
                    int index = Games.IndexOf(game);
                    if (index != -1)
                    {
                        // Remove the old game from the collection
                        Games.RemoveAt(index);
                        // Insert the updated game at the same index
                        Games.Insert(index, game);
                    }

                    // Display success message
                    await DisplayAlert("Success", "Game updated successfully.", "OK");
                }
            }
        }
        

    }
}
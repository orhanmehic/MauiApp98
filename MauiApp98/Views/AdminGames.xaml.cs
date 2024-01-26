using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public AdminGames()
        {
            InitializeComponent();
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
            SqliteData database = new SqliteData(dbPath);
            gameService = new GameService(database);
            Games = new ObservableCollection<Games>(gameService.getAllGames());
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
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp98.Data;
using MauiApp98.Models;
using MauiApp98.Services;

namespace MauiApp98.Views;

public partial class AdminUsers : ContentPage
{
    
    private UserService userService;
    public ObservableCollection<Users> Users { get; } // Make the property read-only

    
    public AdminUsers()
    {
        InitializeComponent();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test.db");
        SqliteData database = new SqliteData(dbPath);
        userService = new UserService(database);
        Users = new ObservableCollection<Users>(userService.getAllUsers());
        BindingContext = this;
    }

    private async void DeleteButton_Clicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Users user)
        {
            // Display a confirmation alert before deleting the user
            bool deleteConfirmed = await DisplayAlert("Confirmation", $"Are you sure you want to delete {user.Username}?", "Yes", "No");

            if (deleteConfirmed)
            {
                // Call the DeleteUser method from UserService to delete the user
                userService.DeleteUser(user);

                // Remove the user from the ObservableCollection
                Users.Remove(user);

                // Display a success alert
                await DisplayAlert("Success", $"{user.Username} has been deleted.", "OK");
            }
            else
            {
                // Display a cancellation alert
                await DisplayAlert("Cancellation", "Deletion operation cancelled.", "OK");
            }
        }
    }


}
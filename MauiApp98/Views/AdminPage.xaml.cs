namespace MauiApp98.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
		
	}

	public void backpage(object sender, EventArgs e)
	{
		Navigation.PopAsync();
	}

	private void adminGames(object? sender, EventArgs e)
	{
		AdminGames adminGames = new AdminGames();
		Navigation.PushModalAsync(adminGames);
	}

	private void adminUsers(object? sender, EventArgs e)
	{
		AdminUsers adminUsers = new AdminUsers();
		Navigation.PushAsync(adminUsers);
	}
	
}
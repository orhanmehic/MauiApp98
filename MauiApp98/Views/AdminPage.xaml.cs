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
}
using MauiApp98.Models;
using System.Diagnostics;

namespace MauiApp98.Views;

public partial class aboutGame : ContentPage
{
    MainPage mainpage {  get; set; }
	public aboutGame(Games game,MainPage mainpageinstance)
	{
		InitializeComponent();
		BindingContext = game;
        mainpage = mainpageinstance;
	}

    public void AddToCartClicked(object sender, EventArgs e)
    {
        if (BindingContext is Games selectedGame)
        {
            mainpage.AddToCart(selectedGame);
        }

    }
}
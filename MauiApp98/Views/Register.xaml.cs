using MauiApp98.Services;
using System.Diagnostics;

namespace MauiApp98.Views;

public partial class Register : ContentPage
{
    private UserService UserService { get; set; }
	public Register(UserService userService)
	{
		InitializeComponent();
        this.UserService = userService;
        
	}

    public void mainpage(Object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    public void RegisterUser(Object sender, EventArgs e)
    {
        if (UsernameEntry.Text == null || EmailEntry.Text == null ||
           PasswordEntry.Text == null || ConfirmPasswordEntry.Text == null)
        {
            UsernameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ConfirmPasswordEntry.Text = string.Empty;
            //prompt
        }
        if(UserService.RegisterUser(UsernameEntry.Text, EmailEntry.Text, PasswordEntry.Text, ConfirmPasswordEntry.Text))
        {
            //prompt for successfull registration
            Navigation.PopToRootAsync();
        }
        else
        {
            UsernameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ConfirmPasswordEntry.Text = string.Empty;
            //prompt
            Debug.WriteLine("didnt work");
        }
    }
}
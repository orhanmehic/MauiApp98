using MauiApp98.Services;

namespace MauiApp98.Views;

public partial class Login : ContentPage
{
	UserService UserService { get; set; }
	public Login(UserService userService)
	{
		InitializeComponent();
		this.UserService = userService;
	}

	public void mainpage(Object sender, EventArgs e)
	{
		Navigation.PopAsync();
	}

	public void LoginUser(object sender, EventArgs e)
	{
		if(UsernameEntry.Text == null || PasswordEntry.Text == null)
		{
			UsernameEntry.Text = string.Empty;
			PasswordEntry.Text = string.Empty;
			//prompt
		}
		var user = UserService.AuthenticateUser(UsernameEntry.Text, PasswordEntry.Text);

        if (user!=null)
		{
			//prompt for succesfull login and session details
			Navigation.PopToRootAsync();
		}
		else
		{
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            //prompt
        }
	}
}
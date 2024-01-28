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
            SecureStorage.SetAsync("username", user.Username);
            //prompt for succesfull login and session details
            Navigation.PopToRootAsync();
		}
        else if(UsernameEntry.Text == "admin" && PasswordEntry.Text == "admin123")
        {
	        Navigation.PushAsync(new AdminPage());
        }
		else
		{
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            //prompt
        }
        
		
	}


	private void RegisterClicked(object? sender, TappedEventArgs e)
	{
		Navigation.PushAsync(new Register(UserService));
	}
}
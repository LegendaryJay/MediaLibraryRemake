using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu.UserDisplay.Login;

public class LoggedInUser
{
    private static readonly Lazy<LoggedInUser> Lazy = new( () => new LoggedInUser());

    public bool IsLoggedIn { get; private set; }
    public User? User { get; private set; }

    public static LoggedInUser Instance => Lazy.Value;
    private LoggedInUser()
    {
    }

    public void Login(User user)
    {
        User = user;
        IsLoggedIn = true;
    }

    public void Logout()
    {
        User = null;
        IsLoggedIn = false;
    }

}
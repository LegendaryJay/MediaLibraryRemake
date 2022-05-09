using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.UserMenu.UserDisplay.Login;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu.UserDisplay;

public class UserDisplay : MenuBase
{
    private User _user;

    public UserDisplay(User user, int level) : base("User Menu", level)
    {
        this._user = user;
        ThisMenu.Configure(x => { x.WriteHeaderAction = () => Console.WriteLine(_user.ToPrettyString()); }
        );
        ThisMenu.Add("Login", () =>
        {
            LoggedInUser.Instance.Login(user);
            ThisMenu.CloseMenu();
            ReadLine.Read("User " + user.Id + " is now logged in");
        });
    }
}
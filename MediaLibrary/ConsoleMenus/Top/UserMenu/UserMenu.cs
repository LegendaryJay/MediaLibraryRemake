using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.UserMenu.Add;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;
using Microsoft.VisualBasic.CompilerServices;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu;

public class UserMenu : DisplayBase<User>
{
    public UserMenu(int level) : base("Users", level)
    {
        ThisMenu.Add("Add User", new AddUser(NextLevel()).Run)
            .Add("Log out", Logout);
    }

    private void Logout()
    {
        var hadUser = LoggedInUser.Instance.User is not null;
        LoggedInUser.Instance.Logout();
        ReadLine.Read(hadUser ? "Logged out" : "No user logged in");
    }
    protected override string DisplayToMenu(User? item)
    {
        return item is not null ? item.ToShortString() : "";
    }

    protected override PageInfo<User> GetPageInfo(PageInfo<User> pageInfo)
    {
        return PageInfo = FileIoSingleton.FileIo.GetPageUsers(pageInfo);
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, User? item)
    {
        var oldUser = LoggedInUser.Instance.User;
        new UserDisplay.UserDisplay(item, NextLevel()).Run();
        var newUser = LoggedInUser.Instance.User;

        if (newUser is not null && (oldUser is null || oldUser.Id != newUser.Id))
        {
            ThisMenu.CloseMenu();
        }
    }
}
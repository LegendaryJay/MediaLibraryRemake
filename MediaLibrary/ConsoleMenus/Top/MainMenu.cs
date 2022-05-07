using ConsoleApp1.ConsoleMenus.Multi_purpose;
using static ConsoleApp1.FileAccessor.FileIoSingleton;

namespace ConsoleApp1.ConsoleMenus.Top;

public class MainMenu : MenuBase
{
    public MainMenu() : base("Main Menu", 0)
    {
        ThisMenu.Add("Movies", () =>
            {
                var mediaMenu = new MovieMenu.MovieMenu(NextLevel());
                mediaMenu.Run();
            }
        );
        ThisMenu.Add("Users", () =>
            {
                new UserMenu.UserMenu(NextLevel()).Run();
            }
        );

    }
}
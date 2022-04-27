using ConsoleApp1.ConsoleMenus.Multi_purpose;

namespace ConsoleApp1.ConsoleMenus.Top;

public class MainMenu : MenuBase
{
    public MainMenu() : base("Main Menu", 0)
    {
        ThisMenu.Add("Movies", () =>
            {
                var mediaMenu = new MediaLibrary.ConsoleMenus.Top.MovieMenu.MovieMenu(NextLevel());
                mediaMenu.Run();
            }
        );
        // .Add("Users", () =>
        //     {
        //         
        //     }
        // )
    }
}
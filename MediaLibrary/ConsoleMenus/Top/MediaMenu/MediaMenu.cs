using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia;
using ConsoleApp1.ConsoleMenus.Top.MediaMenu.DisplayMedia;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu;

public class MediaMenu : MenuBase
{
    public MediaMenu() : base($"Movie Options", 1)
    {
        
        ThisMenu.Add("Display All", () => { new MediaDisplayMenu().Run(); }
            )
            .Add("Add to File", () => { new AddMenu().Run(); }
            )
            .Add("Search", () => { new SearchMedia(0).Run(); }
            );
    }
}
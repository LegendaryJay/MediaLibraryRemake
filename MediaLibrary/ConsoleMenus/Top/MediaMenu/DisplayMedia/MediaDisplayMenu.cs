using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.DisplayMedia;

public class MediaDisplayMenu : DisplayMenu
{
    public MediaDisplayMenu() : base(FileIoSingleton.Instance.FileIo.GetAllMovies(),
        x => x.ToPrettyString(), $"Movies Display", 2)
    {
    }
}
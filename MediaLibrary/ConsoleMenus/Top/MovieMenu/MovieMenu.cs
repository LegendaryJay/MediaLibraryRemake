using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : DisplayBase<Movie>
{
    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        var movie = GetItem(thisMenu.CurrentItem.Index - 1);
        if (movie is not null)
        {
            new EditMenu(movie).Run();
        }
    }

    public MovieMenu() : base(FileIoSingleton.Instance.FileIo.GetAllMovies(), "Movie Menu", 1)
    {
        ThisMenu
            .Add("Add", () => { })
            .Add("Sort", () => { })
            .Add("Filter", () => { })
            .Add("Analyze", () => { });
    }

    protected override string DisplayToMenu(Movie item)
    {
        return item.ToShortString();
    }
}
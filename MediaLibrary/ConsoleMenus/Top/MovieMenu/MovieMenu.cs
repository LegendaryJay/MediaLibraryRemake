using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : DisplayBase<Movie>
{
    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        var index = thisMenu.CurrentItem.Index - 1;
        var movie = GetItem(index);
        if (movie is null) return;
        new EditMenu(movie).Run();
        thisMenu.CurrentItem.Name = DisplayMenuName(index);


    }

    public MovieMenu() : base(FileIoSingleton.Instance.FileIo.GetAllMovies(), "Movie Menu", 1)
    {
        ThisMenu
            .Add("Add", () => new EditMenu().Run())
            .Add("Sort", () => { })
            .Add("Filter", () => { })
            .Add("Analyze", () => { });
    }

    protected override string DisplayToMenu(Movie item)
    {
        return item.ToShortString();
    }
}
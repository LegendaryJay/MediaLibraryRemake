using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : DisplayBase<Movie>
{
    public MovieMenu(int level) : this(FileIoSingleton.FileIo.GetAllMovies(), "Movie Menu", level)
    {
    }
    public MovieMenu(List<Movie> movies, string title, int level) : base(movies, title, level)
    {
        ThisMenu
            .Add("Add", () =>
            {
                new EditMenu(NextLevel()).Run(out var movie);
                if (movie is not null) IndexTracker.Items.Add(movie);
            })
            .Add("Sort", () =>
            {
                new SortMenu.SortMenu(IndexTracker, Level + 1).Run();
                UpdatePage();
            })
            .Add("Filter", () => { new FilterMenu.FilterMenu(IndexTracker, Title, Level).Run(); })
            .Add("(not implemented) Analyze", () => { });
    }



    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        var tracker = IndexTracker.GetTrackerObject(thisMenu.CurrentItem.Index - 1);
        if (!tracker.isValid) return;
        new EditMenu(tracker.Item!, IndexTracker, NextLevel()).Run();
        thisMenu.CurrentItem.Name = DisplayMenuName(tracker);
        UpdatePage();

    }

    protected override string DisplayToMenu(TrackerObject<Movie?> tracker)
    {
        return tracker.Item!.ToShortString();
    }
}
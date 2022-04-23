using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : DisplayBase<Movie>
{
    public string Title { get; }
    public int Level { get; }

    public MovieMenu() : this(FileIoSingleton.Instance.FileIo.GetAllMovies(), "Movie Menu", 1)
    {
    }
    public MovieMenu(List<Movie> movies, string title, int level) : base(movies, title, level)
    {
        Level = level;
        Title = title;   
        ThisMenu
            .Add("Add", () =>
            {
                new EditMenu().Run(out var movie);
                if (movie is not null) IndexTracker.Items.Add(movie);
            })
            .Add("(not implemented) Sort", () => { })
            .Add("Filter", () => { new FilterMenu.FilterMenu(IndexTracker, Title, Level).Run(); })
            .Add("(not implemented) Analyze", () => { });
    }



    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        var tracker = IndexTracker.GetTrackerObject(thisMenu.CurrentItem.Index - 1);
        if (!tracker.isValid) return;
        new EditMenu(tracker.Item!, IndexTracker).Run();
        thisMenu.CurrentItem.Name = DisplayMenuName(tracker);
        UpdatePage();

    }

    protected override string DisplayToMenu(TrackerObject<Movie?> tracker)
    {
        return tracker.Item!.ToShortString();
    }
}
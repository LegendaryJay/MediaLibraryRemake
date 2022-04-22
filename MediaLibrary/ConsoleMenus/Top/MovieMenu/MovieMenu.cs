using System.Globalization;
using System.Xml;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : DisplayBase<Movie>
{
    public MovieMenu() : base(FileIoSingleton.Instance.FileIo.GetAllMovies(), "Movie Menu", 1)
    {
        ThisMenu
            .Add("Add", () =>
            {
                new EditMenu().Run(out Movie? movie);
                if (movie is not null) IndexTracker.Items.Add(movie);
            })
            .Add("Sort", () => { })
            .Add("Filter", () => { })
            .Add("Analyze", () => { });
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
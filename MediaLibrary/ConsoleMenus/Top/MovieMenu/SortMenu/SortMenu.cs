using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.SortMenu;

public class SortMenu : MenuBase
{
    private readonly ItemIndexTracker<Movie> _tracker;

    
    private void OnPress<T>(Func<Movie, T> toComparable) where T : IComparable
    {
        _tracker.Items.Sort((x, y) => toComparable(x).CompareTo(toComparable(y)));
        ThisMenu.CloseMenu();
    }

    public SortMenu(ItemIndexTracker<Movie> tracker, int level) : base("Sort By", level)
    {
        _tracker = tracker;
        ThisMenu
            .Add("Id", () => OnPress(x => x.Id))
            .Add("Title", () => OnPress(x => x.Title))
            .Add("ReleaseDate", () => OnPress(x => x.ReleaseDate))
            .Add("Rating", () => OnPress(x => -1 * x.UserMovies.Average(y => y.Rating)));
    }
}
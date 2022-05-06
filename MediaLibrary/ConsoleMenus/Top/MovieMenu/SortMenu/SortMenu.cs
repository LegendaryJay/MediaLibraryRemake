using System.ComponentModel;
using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.SortMenu;

public class SortMenu : MenuBase
{
    private Func<Movie, object> _orderBy = movie => movie.Id;
    private ListSortDirection _direction = ListSortDirection.Ascending;

    
    private void OnPress(Func<Movie, object> orderBy, ListSortDirection direction)
    {
        _orderBy = orderBy;
        _direction = direction;
        ThisMenu.CloseMenu();
    }

    public SortMenu(int level) : base("Sort By", level)
    {
        ThisMenu
            .Add("Id", () => OnPress(x => x.Id, ListSortDirection.Ascending))
            .Add("Title", () => OnPress(x => x.Title, ListSortDirection.Ascending))
            .Add("ReleaseDate", () => OnPress(x => x.ReleaseDate, ListSortDirection.Ascending))
            .Add("Rating", () => OnPress(x => x.UserMovies.IsNullOrEmpty() ? 0 :  -1 * x.UserMovies.Average(y => y.Rating), ListSortDirection.Descending));
    }

    public void Run(out Func<Movie, object> orderBy, out ListSortDirection direction)
    {
        Run();
        orderBy = _orderBy;
        direction = _direction;
    }
}
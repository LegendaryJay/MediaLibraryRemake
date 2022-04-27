using System.Linq.Expressions;
using ConsoleApp1.FileAccessor.Database.Context;
using ConsoleApp1.MediaEntities;
using ConsoleTools;
using MediaLibrary.ConsoleMenus.Multi_purpose;

namespace MediaLibrary.ConsoleMenus.Top.MovieMenu;


public class MovieMenu : DisplayBase<Movie>
{
    private Expression<Func<Movie, object>> _orderBy = x => x.Id;
    
    public MovieMenu(int level) : base("Movie Menu", level)
    {
        // ThisMenu
        //     .Add("Add", () =>
        //     {
        //         new EditMenu(NextLevel()).Run();
        //         Update();
        //     })
        //     .Add("Sort", () =>
        //     {
        //         new SortMenu.SortMenu(NextLevel()).Run();
        //         Update();
        //     })
        //     .Add("Filter", () =>
        //     {
        //         new FilterMenu.FilterMenu(Title, NextLevel()).Run();
        //         Update();
        //     })
        //     .Add("(not implemented) Analyze", () => { });
    }


    protected override List<Movie> GetPageItems()
    {
        using var unitOfWork = new UnitOfWork(new MovieContext());
        return unitOfWork.Movies.GetDetailedMovies(PageIndex, PageSize).ToList();
    }

    protected override int GetTotalItemsCount()
    {
        using var unitOfWork = new UnitOfWork(new MovieContext());
        return unitOfWork.Movies.GetMovieCount();
    }

    protected override string DisplayToMenu(Movie item)
    {
        return item.ToShortString();
    }

    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        // var tracker = IndexTracker.GetTrackerObject(thisMenu.CurrentItem.Index - 1);
        // if (!tracker.isValid) return;
        // new EditMenu(tracker.Item!, IndexTracker, NextLevel()).Run();
        // thisMenu.CurrentItem.Name = GetMenuDisplayName(tracker);
        // UpdatePage();

    }
}
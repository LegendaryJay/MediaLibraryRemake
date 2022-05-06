using System.ComponentModel;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : DisplayBase<Movie>
{
    private string _filterToString = "";
    private Func<Movie, object> _orderBy = movie => movie.Id;
    private ListSortDirection _direction = ListSortDirection.Ascending;
    private Func<Movie, bool> _where = movie => true;

    public MovieMenu(int level) : base("Movie Menu", level)
    {
        ThisMenu
            .Add("Add", AddMovie)
            .Add("Sort", SortMovies)
            .Add("Filter", FilterMovies)
            .Add("(not implemented) Analyze", () => { });
    }

    public void AddMovie()
    {
        new EditMenu(NextLevel()).Run();
        UpdatePage();
    }

    public void SortMovies()
    {
        new SortMenu.SortMenu(NextLevel()).Run(out _orderBy, out _direction);
        PageInfo.ResetPage();
        UpdatePage();
    }

    public void FilterMovies()
    {
        new FilterMenu.FilterMenu( _filterToString, _where, NextLevel()).Run(out _filterToString, out _where);
        PageInfo.ResetPage();
        UpdatePage();
    }

    protected override string DisplayToMenu(Movie? item)
    {
        return item is null ? "" : item.ToShortString();
    }
    

    protected override PageInfo<Movie> GetPageInfo(PageInfo<Movie> pageInfo)
    {
        return PageInfo = FileIoSingleton.FileIo.GetPageMovies(pageInfo, _orderBy, _direction, _where);
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, Movie? item)
    {
        new EditMenu(item, NextLevel()).Run();
        UpdatePage();
    }
}
using System.Net.NetworkInformation;
using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.FilterMenu;

public class FilterMenu : MenuBase
{
    private const string Prefix = "| ";
    private string _filterToString;
    private Func<Movie, bool> _where;

    private void FilterByTitle()
    {
        var filterBy = ReadLine.Read("What key words do you want to filter By?");
        if (filterBy is null) return;
        SetStringFilter(filterBy);
        var filterByList = filterBy.Split();
        _where = m => filterByList.All(searchTerm => m.Title.ToLower().Contains(searchTerm.ToLower()));
        ThisMenu.CloseMenu();
    }

    private void SetStringFilter(string str)
    {
        _filterToString = str.IsNullOrEmpty() ? "" : Prefix + str;
    }

    private void FilterByYear()
    {
        var filterBy = ReadLine.Read("What year do you want to filter By?");
        if (filterBy is null || filterBy.Length != 4 || !int.TryParse(filterBy, out var yearInt)) return;
        _where = m => m.ReleaseDate.Year == yearInt || m.Title.Contains($"({yearInt})");
        SetStringFilter("Year: " + filterBy);
        ThisMenu.CloseMenu();
    }

    private void FilterByRated()
    {
        var user = LoggedInUser.Instance.User; 
        _where = m =>
        {
            return m.UserMovies is not null && m.UserMovies.Select(x => x.UserId).Contains(user.Id);
        };
        SetStringFilter("Rated by User " + user.Id);
        ThisMenu.CloseMenu();
    }

    private void FilterByGenres()
    {
        var movie = new Movie();
        new GenreMenu(movie, NextLevel()).Run();
        if (!movie.MovieGenres.IsNullOrEmpty()) return;
        var filterByIdList = movie.MovieGenres.Select(x => x.Id);
        _where = m =>
            filterByIdList.All(filterId =>
                m.MovieGenres
                    .Select(
                        mg => mg.GenreId)
                    .Contains(filterId)
            );
        SetStringFilter($"Genres {string.Join(", ", movie.MovieGenres.Select(x => x.Genre.Name))}");
        ThisMenu.CloseMenu();
    }


    public FilterMenu(string filterToString, Func<Movie, bool> where, int level) : base("Filter Menu",
        level)
    {
        _where = where;
        _filterToString = filterToString;

        const string prefix = "Filter by ";
        ThisMenu.Add(prefix + "Title", FilterByTitle)
            .Add(prefix + "Year", FilterByYear)
            .Add(prefix + "Genre", FilterByGenres);
        if (LoggedInUser.Instance.IsLoggedIn)
        {
            ThisMenu.Add(prefix + "Rated", FilterByRated);
        }
    }

    public void Run(out string filterToString, out Func<Movie, bool> where)
    {
        Run();
        filterToString = _filterToString;
        where = _where;
    }
}
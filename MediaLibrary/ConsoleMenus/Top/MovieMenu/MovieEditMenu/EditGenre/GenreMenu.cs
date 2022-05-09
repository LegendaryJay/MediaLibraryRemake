using System.Collections.Specialized;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;

public class GenreMenu : DisplayBase<GenreDummy>
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private const string SelectedString = " * ";
    private readonly List<GenreDummy> _genres = new();
    private readonly Movie _movie;


    public GenreMenu(Movie? movie, int level) : base("Choose Genres", level)
    {
        _movie = movie ?? new Movie();
        _movie.MovieGenres ??= new List<MovieGenres>();
        var genres = FileIoSingleton.FileIo.GetAllGenres();
        PageInfo.TotalItemCount = genres.Count;
        var movieGenreList = _movie.MovieGenres.Select(x => x.Genre.Id).ToList();

        foreach (var genre in genres)
        {
            _genres.Add(new GenreDummy(genre, movieGenreList.Contains(genre.Id)));
        }

        ThisMenu.Add("Save Changes", thisMenu =>
        {
            SaveResult();
            thisMenu.CloseMenu();
        });
    }

    public GenreMenu(int level) : this(new Movie(), level)
    {
    }

    private void SaveResult()
    {
        _movie.MovieGenres.Clear();
        foreach (var genre in _genres.Where(genre => genre.IsRecorded))
        {
            _movie.MovieGenres.Add(new MovieGenres
            {
                MovieId = _movie.Id,
                GenreId = genre.Id,
                Movie = _movie,
                Genre = genre.Genre
            });
        }

        FileIoSingleton.FileIo.UpdateMovie(_movie);
        logger.Info("User chose desired genres: " + string.Join(", ", _genres.Where(x => x.IsRecorded).Select(x => x.Name)));
    }

    protected override string PrependToName(GenreDummy? item)
    {
        return item!.IsRecorded ? SelectedString : "";
    }


    private void ToggleGenre(GenreDummy? item)
    {
        if (item is null) return;
        item.IsRecorded = !item.IsRecorded;
    }

    protected override string DisplayToMenu(GenreDummy item)
    {
        return item.Name ?? "";
    }

    protected override PageInfo<GenreDummy> GetPageInfo(PageInfo<GenreDummy> pageInfo)
    {
        pageInfo.Items = _genres.Skip(pageInfo.PageIndex * pageInfo.PageLength).Take(pageInfo.PageLength).ToList() ??
                         new List<GenreDummy>();
        return pageInfo;
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, GenreDummy? item)
    {
        if (item is null) return;
        ToggleGenre(item);
        thisMenu.CurrentItem.Name = DisplayMenuName(item);
    }
}
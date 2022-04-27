using System.Reflection;
using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;

public abstract class BaseEditMenu : MenuBase
{
    private static readonly List<string> MenuTitle =
    {
        "Title",
        "Release Year",
        "Genre"
    };

    protected void AddMenuItem(string title, Action onClick)
    {
        ThisMenu.Add(title, onClick);
        MenuTitle.Add(title);
    }
    protected void AddMenuItem(string title, Action<ConsoleMenu> onClick)
    {
        ThisMenu.Add(title, onClick);
        MenuTitle.Add(title);
    }
    private void Initialize()
    {
        foreach (var VARIABLE in COLLECTION)
        {
            
        }
        ThisMenu.Add()
        
    }
    private readonly Movie _movie;
   
    
    private readonly string _actionWord;


    public void UpdateMovie(Movie movie)
    {
        //get movie
        //set title/release
        //setMovieGenres
        //setRatings
    }
    
    
    //validate
    //ifValid, set original

    protected virtual void AddToMenuOnStartup()
    {
        
    }
    
    protected abstract void SaveMovieToDatabase();
    
    private EditMenu(Movie movie, string title, int level) : base(title, level)
    {
        _movie = movie;

        ThisMenu.Add($"{_actionWord} Title", SetTitle);
        ThisMenu.Add($"{_actionWord} ReleaseDate", SetReleaseDate);
        ThisMenu.Add($"{_actionWord} Genre", SetGenres);
        AddToMenuOnStartup();
        ThisMenu.Add("Confirm Changes", SaveMovieToDatabase);
    }

    private string GetName(int index, string newValue)
    {
        var extra = newValue.IsNullOrEmpty() ? "" : $" - (edited to {newValue})";

        return $"{_actionWord} {MenuName[index]}{extra}";
    }


    
    
    private void SetTitle()
    {
        ReadLine.ClearHistory();
        ReadLine.AddHistory(_movie.Title ?? "");
        var newTitle = ReadLine.Read("Change Title to: ").Trim();

        OnValidate(
            0,
            ValidateMovies.ValidateTitle(newTitle) && !newTitle.Equals(_originalMovie.Title),
            newTitle,
            () => _editableMovie.Title = newTitle
        );
    }

    private void SetReleaseDate()
    {
        ReadLine.ClearHistory();
        if (_editableMovie.ReleaseDate != DateTime.MinValue)
            ReadLine.AddHistory(
                (_editableMovie.ReleaseDate != DateTime.MinValue)
                    ? _editableMovie.ReleaseDate.Year.ToString()
                    : DateTime.Now.Year.ToString());
        var newYear = ReadLine.Read("Change Release Year to: ").Trim();

        OnValidate(
            1,
            ValidateMovies.ValidateYear(newYear, out var year) && year != _originalMovie.ReleaseDate,
            year.Year.ToString(),
            () => _editableMovie.ReleaseDate = year
        );
    }

    private void SetGenres()
    {
        new GenreMenu(_editableMovie).Run(out var genres);

        OnValidate(
            2,
            ValidateMovies.ValidateGenres(genres)
            && (genres.Count != _originalMovie.MovieGenres.Count
                || !genres.Select(x => x.Id)
                    .All(
                        x => _originalMovie.MovieGenres.Select(y => y.GenreId).Contains(x)
                    )
            ),
            string.Join(" ,", genres.Select(x => x.Name)),
            () => _editableMovie.MovieGenres = genres.Select(x => new MovieGenres
            {
                MovieId = _originalMovie.Id,
                Movie = _originalMovie,
                GenreId = x.Id,
                Genre = x
            }).ToList()
        );
    }

    public void Run(out Movie? movie)
    {
        this.Run();
        movie = _outputMovie;
    }
}
using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu;

public class EditMenu : MenuBase
{
    private static readonly string[] MenuName =
    {
        "Title",
        "Release Year",
        "Genre"
    };

    private readonly Movie _editableMovie;
    private readonly Movie _originalMovie;
    private readonly string _actionWord;
    private Movie? _outputMovie;

    private ItemIndexTracker<Movie> indexTracker;

    private EditMenu(bool isNew, Movie movie, string title) : base(title, 2)
    {
        Action<ConsoleMenu> onSave;
        _editableMovie = movie;

        if (isNew)
        {
            _originalMovie = new Movie
            {
                Title = "",
                ReleaseDate = DateTime.MinValue,
                MovieGenres = new List<MovieGenres>()
            };
            _actionWord = "Add";
            onSave = thisMenu =>
            {
                if (ValidateMovies.ValidateMovie(movie))
                {
                    FileIoSingleton.Instance.FileIo.AddMovie(movie);
                    _outputMovie = movie;
                    thisMenu.CloseMenu();
                }
                else
                {
                    thisMenu.Configure(x => x.Title = "Movie Not complete");
                }
            };
        }
        else
        {
            _actionWord = "Edit";
            _originalMovie = new Movie
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                MovieGenres = movie.MovieGenres
            };
            onSave = thisMenu =>
            {
                FileIoSingleton.Instance.FileIo.UpdateMovie(movie);
                thisMenu.CloseMenu();
            };
        }

        ThisMenu.Add($"{_actionWord} {MenuName[0]}", SetTitle);
        ThisMenu.Add($"{_actionWord} {MenuName[1]}", SetReleaseDate);
        ThisMenu.Add($"{_actionWord} {MenuName[2]}", SetGenres);
        ThisMenu.Add($"!!Delete!!", (thisMenu) =>
            {
                new VerifyMenu(
                    "Are you sure you want to Delete " + movie.Title + "?",
                    2,
                    () =>
                    {
                        FileIoSingleton.Instance.FileIo.DeleteMovie(movie.Id);
                        indexTracker?.Items.Remove(movie);
                    }
                ).Run();
                ThisMenu.CloseMenu();
            }
        );
        ThisMenu.Add("Save and Exit", onSave);
    }

    public EditMenu(Movie movie, ItemIndexTracker<Movie> indexTracker) : this(false, movie, movie.ToPrettyString())
    {
        this.indexTracker = indexTracker;
    }

    public EditMenu() : this(true, new Movie(), "Add New Movie")
    {
    }

    private string GetName(int index, string newValue)
    {
        var extra = newValue.IsNullOrEmpty() ? "" : $" - (edited to {newValue})";

        return $"{_actionWord} {MenuName[index]}{extra}";
    }

    private void OnValidate(int index, bool isChanged, string input, Action action)
    {
        var displayString = "";
        if (isChanged)
        {
            action();
            displayString = input;
        }

        ThisMenu.Items[index + 1].Name = GetName(index, displayString);
    }


    private void SetTitle()
    {
        ReadLine.ClearHistory();
        ReadLine.AddHistory(_editableMovie.Title ?? "");
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
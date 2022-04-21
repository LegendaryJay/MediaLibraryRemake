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
    private readonly string actionWord;
    private readonly bool isNew;

    private EditMenu(bool isNew, Movie movie, string title) : base(title, 2)
    {
        Action<ConsoleMenu> onSave;
        _editableMovie = movie;

        this.isNew = isNew;
        if (isNew)
        {
            _originalMovie = new Movie
            {
                Title = "",
                ReleaseDate = DateTime.MinValue,
                MovieGenres = new List<MovieGenres>()
            };
            actionWord = "Add";
            onSave = thisMenu =>
            {
                if (ValidateMovies.ValidateMovie(movie))
                {
                    FileIoSingleton.Instance.FileIo.AddMovie(movie);
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
            actionWord = "Edit";
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

        ThisMenu.Add($"{actionWord} {MenuName[0]}", SetTitle);
        ThisMenu.Add($"{actionWord} {MenuName[1]}", SetReleaseDate);
        ThisMenu.Add($"{actionWord} {MenuName[2]}", SetGenres);
        ThisMenu.Add("Save and Exit", onSave);
    }

    public EditMenu(Movie movie) : this(false, movie, movie.ToPrettyString())
    {
    }

    public EditMenu() : this(true, new Movie(), "Add New Movie")
    {
    }

    private string GetName(int index, string newValue)
    {
        var extra = newValue.IsNullOrEmpty() ? "" : $" - (edited to {newValue})";

        return $"{actionWord} {MenuName[index]}{extra}";
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
        ReadLine.AddHistory(_editableMovie.Title);
        var newTitle = ReadLine.Read("Change Title to: ", _editableMovie.Title).Trim();

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
        ReadLine.AddHistory($"{_editableMovie.ReleaseDate.Year}");
        var newYear = ReadLine.Read("Change Release Year to: ", _editableMovie.Title).Trim();

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
                GenreId = x.Id,
            }).ToList()
        );
    }
}
using System.Data.Entity.Infrastructure.Design;
using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.Rate;
using ConsoleApp1.ConsoleMenus.Top.UserMenu.UserDisplay.Login;
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

    private readonly Movie _movie;
    private readonly Movie _movieChanges;
    private readonly string _actionWord;
    private readonly Action _saveType;

    private void Update()
    {
        ThisMenu.Configure(config =>
            {
                var ratingString = "";
                if (LoggedInUser.Instance.IsLoggedIn)
                {
                    ReadLine.Read(_movie.UserMovies.ToString());
                    var loggedInUser = LoggedInUser.Instance.User;
                    ratingString = "";
                    if (loggedInUser is not null)
                    {
                        var rating = _movie.UserMovies.FirstOrDefault(x => x.UserId == loggedInUser.Id);
                        ratingString = "\n\tUser Rating: " + (rating is not null ? $"{rating.Rating}" : "_") + " / 5";
                    }
                }

                config.WriteHeaderAction = () => Console.WriteLine(_movieChanges.ToPrettyString() + ratingString);
            }
        );
    }

    private void SaveChanges()
    {
        if (ValidateMovies.ValidateMovie(_movieChanges))
        {
            _movie.Title = _movieChanges.Title;
            _movie.ReleaseDate = _movieChanges.ReleaseDate;
            _movie.MovieGenres.Clear();
            foreach (var movieGenre in _movieChanges.MovieGenres)
            {
                _movie.MovieGenres.Add(new MovieGenres
                {
                    MovieId = _movie.Id,
                    GenreId = movieGenre.GenreId,
                    Movie = _movie,
                    Genre = movieGenre.Genre
                });
            }

            _saveType();
            ThisMenu.CloseMenu();
        }
        else
        {
            ReadLine.Read("Movie not Valid");
        }
    }

    private void SetRating()
    {
        new RateMenu(_movie, NextLevel()).Run();
        Update();
    }

    private EditMenu(bool isNew, Movie movie, string title, int level) : base(title, level)
    {
        Action<ConsoleMenu> onSave;
        _movie = movie ?? new Movie();
        _movieChanges = new Movie
        {
            Id = _movie.Id,
            Title = _movie.Title ?? "",
            ReleaseDate = _movie.ReleaseDate,
            MovieGenres = new List<MovieGenres>(_movie.MovieGenres ?? new List<MovieGenres>()),
            UserMovies = new List<UserMovie>(_movie.UserMovies ?? new List<UserMovie>())
        };


        if (isNew)
        {
            _actionWord = "Add";
            _saveType = () => FileIoSingleton.FileIo.AddMovie(_movie);
        }
        else
        {
            _actionWord = "Edit";
            _saveType = () => FileIoSingleton.FileIo.UpdateMovie(_movie);
        }

        ThisMenu.Add($"{_actionWord} {MenuName[0]}", SetTitle);
        ThisMenu.Add($"{_actionWord} {MenuName[1]}", SetReleaseDate);
        ThisMenu.Add($"{_actionWord} {MenuName[2]}", SetGenres);

        if (!isNew)
        {
            if (LoggedInUser.Instance.IsLoggedIn)
                ThisMenu.Add($"Rate", SetRating);
            ThisMenu.Add($"!!Delete!!", () =>
                {
                    new VerifyMenu(
                        "Are you sure you want to Delete " + movie.Title + "?",
                        2,
                        () =>
                        {
                            FileIoSingleton.FileIo.DeleteMovie(movie.Id);
                            ThisMenu.CloseMenu();
                        }
                    ).Run();
                }
            );
        }

        ThisMenu.Add("Save and Exit", SaveChanges);
    }

    public EditMenu(int level) : this(true, new Movie(), "Add New Movie", level)
    {
    }

    public EditMenu(Movie movie, int level) : this(false, movie, "Edit Movie", level)
    {
    }

    private string GetName(int index, string newValue)
    {
        var extra = newValue.IsNullOrEmpty() ? "" : $" : {newValue}";

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
        Update();
    }


    private void SetTitle()
    {
        ReadLine.ClearHistory();
        ReadLine.AddHistory(_movieChanges.Title ?? "");
        var newTitle = ReadLine.Read("Change Title to: ").Trim();

        OnValidate(
            0,
            ValidateMovies.ValidateTitle(newTitle) && !newTitle.Equals(_movieChanges.Title),
            newTitle,
            () => _movieChanges.Title = newTitle
        );
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () => Console.WriteLine(_movieChanges.ToPrettyString());
            }
        );
    }

    private void SetReleaseDate()
    {
        ReadLine.ClearHistory();
        if (_movieChanges.ReleaseDate != DateTime.MinValue)
            ReadLine.AddHistory(
                (_movieChanges.ReleaseDate != DateTime.MinValue)
                    ? _movieChanges.ReleaseDate.Year.ToString()
                    : DateTime.Now.Year.ToString());
        var newYear = ReadLine.Read("Change Release Year to: ").Trim();

        OnValidate(
            1,
            ValidateMovies.ValidateYear(newYear, out var year) && year != _movieChanges.ReleaseDate,
            year.Year.ToString(),
            () => _movieChanges.ReleaseDate = year
        );
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () => Console.WriteLine(_movieChanges.ToPrettyString());
            }
        );
    }

    private void SetGenres()
    {
        var tempMovie = new Movie
        {
            Id = _movieChanges.Id,
            Title = _movieChanges.Title,
            ReleaseDate = _movieChanges.ReleaseDate,
            MovieGenres = new List<MovieGenres>(_movieChanges.MovieGenres),
        };
        new GenreMenu(tempMovie, NextLevel()).Run();

        var genres = tempMovie.MovieGenres.ToList();

        OnValidate(
            2,
            ValidateMovies.ValidateGenres(genres)
            && (genres.Count != _movieChanges.MovieGenres.Count
                || !genres.Select(x => x.Id)
                    .All(
                        x => _movieChanges.MovieGenres.Select(y => y.GenreId).Contains(x)
                    )
            ),
            string.Join(" ,", genres.Select(x => x.Genre.Name)),
            () => _movieChanges.MovieGenres = genres.Select(x => new MovieGenres
            {
                MovieId = _movieChanges.Id,
                Movie = _movieChanges,
                GenreId = x.GenreId,
                Genre = x.Genre
            }).ToList()
        );
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () => Console.WriteLine(_movieChanges.ToPrettyString());
            }
        );
    }

    public override void Run()
    {
        Update();
        base.Run();
    }
}
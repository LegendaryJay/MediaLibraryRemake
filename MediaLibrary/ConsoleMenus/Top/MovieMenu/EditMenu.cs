using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Top.MovieMenu;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class EditMenu : MenuBase
{
    private Movie editableMovie;

    private void SetTitle()
    {
        ReadLine.ClearHistory();
        ReadLine.AddHistory(editableMovie.Title);
        var newTitle = ReadLine.Read("Change Title to: " ,editableMovie.Title).Trim();
        if (ValidateMovies.ValidateTitle(newTitle))
        {
            editableMovie.Title = newTitle;
        }

        ThisMenu.Items[1].Name = "Edit Title - " + newTitle;
    }

    private void SetReleaseDate()
    {
        ReadLine.ClearHistory();
        ReadLine.AddHistory($"{editableMovie.ReleaseDate.Year}");
        var newYear = ReadLine.Read("Change Release Year to: " ,editableMovie.Title).Trim();
        if (ValidateMovies.ValidateYear(newYear, out var year))
        {

            editableMovie.ReleaseDate = year;
        }

        ThisMenu.Items[2].Name = "Edit Release Year- " + year.Year;
    }
    public EditMenu(Movie movie) : base(movie.ToPrettyString(), 2)
    {
        editableMovie = movie;
        ThisMenu.Add("Edit Title", SetTitle);
        ThisMenu.Add("Edit Release Year", SetReleaseDate);
        ThisMenu.Add("Edit Genres", new GenreMenu(movie).Run);
        ThisMenu.Add("Save Changes", () => FileIoSingleton.Instance.FileIo.UpdateMovie(movie));
    }
}
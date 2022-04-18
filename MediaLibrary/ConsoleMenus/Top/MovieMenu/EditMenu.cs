using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class EditMenu : MenuBase
{
    public EditMenu(Movie movie) : base(movie.ToPrettyString(), 2)
    {
        ThisMenu.Add("Edit", () => Console.WriteLine(""));
    }
}
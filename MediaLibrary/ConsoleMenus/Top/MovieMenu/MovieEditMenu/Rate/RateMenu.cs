using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.Rate;

public class RateMenu : MenuBase
{
    private Movie _movie;

    public RateMenu(Movie movie, int level) : base("What is your rating?", level)
    {
        _movie = movie;
        
        ThisMenu.Add("Rate 1", () => Rate(1));
        ThisMenu.Add("Rate 2", () => Rate(2));
        ThisMenu.Add("Rate 3", () => Rate(3));
        ThisMenu.Add("Rate 4", () => Rate(4));
        ThisMenu.Add("Rate 5", () => Rate(5));
        ThisMenu.Add("Delete Rating", () => Rate(0));
    }
    
    private void Rate(int i)
    {
        FileIoSingleton.FileIo.Rate(LoggedInUser.Instance.User.Id, _movie.Id, i);
        ThisMenu.CloseMenu();
    }
}
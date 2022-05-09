using ConsoleApp1.ConsoleMenus.Multi_purpose;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze;

public class AnalyzeMenu : MenuBase
{
    public AnalyzeMenu(int level) : base("Special Filters", level)
    {
        ThisMenu.Add(
                "Movies By Occupation",
                () =>
                {
                    ThisMenu.CloseMenu();
                    new MovieOccupationDisplayMenu.MovieOccupationDisplayMenu(NextLevel()).Run();
                }
            )
            .Add("Year errors", () =>
            {
                ThisMenu.CloseMenu();
                new MovieYearErrorDisplayMenu.MovieYearErrorDisplayMenu(NextLevel()).Run();
            });
    }
}
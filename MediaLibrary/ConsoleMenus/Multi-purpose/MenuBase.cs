using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public abstract class MenuBase
{
    protected readonly ConsoleMenu ThisMenu;
    protected readonly string Title;
    protected readonly int Level;
    protected int NextLevel()
    {
        return Level + 1;
    }

    protected MenuBase(string title, int level)
    {
        Title = title;
        Level = level;
        
        ThisMenu = new ConsoleMenu(Array.Empty<string>(), level)
            .Add("Back\n", ConsoleMenu.Close)
            .Configure(
                config =>
                {
                    config.EnableBreadcrumb = true;
                    config.Title = title;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" > ", titles));
                }
            );
    }

    public virtual void Run()
    {
        ThisMenu.Show();
    }
}
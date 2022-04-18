using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class MovieMenu : MenuBase
{
    private const int ItemsPerPage = 5;
    private readonly List<Movie> _items;
    private readonly int _resultCount;
    private int _page;

    private int GetMovieIndex(int index)
    {
        return index + _page * ItemsPerPage;
    }
    private Movie? GetMovie(int index)
    {
        var realIndex = GetMovieIndex(index);
        return realIndex < _items.Count ? _items[realIndex] : null;

    }

    public MovieMenu() : base("Movie Menu", 1)
    {
        _items = FileIoSingleton.Instance.FileIo.GetAllMovies();
        _resultCount = _items.Count;
        for (var i = 0; i < ItemsPerPage; i++)
        {
            ThisMenu
                .Add("Movie " + i, () =>
                {
                    new EditMenu(GetMovie(i)).Run();
                });
        }

        ThisMenu
            .Add("Previous", Previous)
            .Add("Next\n", Next)
            .Add("Add", () => { })
            .Add("Sort", () => { })
            .Add("Filter", () => { })
            .Add("Analyze", () => { });
        UpdatePage();
    }

    private void ChangePage(int direction)
    {
        _page = (_page + direction + GetPageCount()) % GetPageCount();
        UpdatePage();
    }

    private void Next()
    {
        ChangePage(1);
    }

    private void Previous()
    {
        ChangePage(-1);
    }

    private int GetPageCount()
    {
        return Math.Max(1, (int) Math.Ceiling(_resultCount / (double) ItemsPerPage));
    }

    private void UpdatePage()
    {
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () => Console.WriteLine($"Page {_page + 1} / {GetPageCount()}\n{_resultCount} items found");
            }
        );
        for (var i = 0; i < ItemsPerPage; i++)
        {
            var movie = GetMovie(i);
            ThisMenu.Items[i + 1].Name = 
                movie is null ? "[Empty]" : movie.ToShortString() + 
                                            (ItemsPerPage -1 == i ? "\n" : "");
        }
    }
    
    public override void Run()
    {
        base.Run();
    }
}
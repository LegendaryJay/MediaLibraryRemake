using System.Text;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class DisplayMenu : MenuBase
{
    private const int ItemsPerPage = 5;
    private readonly List<Movie> _items;
    private readonly int _resultCount;
    private readonly Func<Movie, string> _toStringFunc;
    private string _cachedPage = "";
    private int _page;

    public DisplayMenu(List<Movie> items, Func<Movie, string> toString, string title, int level) : base(title, level)
    {
        _items = items;
        _toStringFunc = toString;
        _resultCount = _items.Count;
        ThisMenu.Add("Previous", Previous)
            .Add("Next", Next)
            .Configure(
                config => { config.WriteHeaderAction = Display; }
            );
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
        var sb = new StringBuilder();
        sb.AppendLine($"Page {_page + 1} / {GetPageCount()}\n{_resultCount} items found");
        for (var i = _page * ItemsPerPage; i < Math.Min((_page + 1) * ItemsPerPage, _items.Count); i++)
            sb.AppendLine(_toStringFunc(_items[i]));
        _cachedPage = sb.ToString();
    }

    private void Display()
    {
        Console.WriteLine(_cachedPage);
    }

    public override void Run()
    {
        base.Run();
        Display();
    }
}
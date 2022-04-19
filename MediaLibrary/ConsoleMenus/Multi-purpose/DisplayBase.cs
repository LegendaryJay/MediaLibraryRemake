namespace ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleTools;

public abstract class DisplayBase<T> : MenuBase
{
    private const int ItemsPerPage = 5;
    protected readonly List<T> _items;
    private readonly int _resultCount;
    private int _page;

    protected abstract string DisplayToMenu(T item);

    protected int GetItemIndex(int index)
    {
        return index + _page * ItemsPerPage;
    }

    protected T? GetItem(int index)
    {
        var realIndex = GetItemIndex(index);
        return realIndex < _items.Count ? _items[realIndex] : default;
    }

    protected string DisplayMenuName(int i)
    {
        var item = GetItem(i);
        if (item is null) return "[empty]";
        return PrependToName(i, item) + DisplayToMenu(item) + AppendToName(i, item);
    }
    protected abstract void RunOnClick(ConsoleMenu thisMenu);

    protected DisplayBase(List<T> items, string title, int level) : base(title, level)
    {
        _items = items;
        _resultCount = _items.Count;
        for (var i = 0; i < ItemsPerPage; i++)
        {
            ThisMenu
                .Add("Item " + i, RunOnClick);
        }

        ThisMenu
            .Add("Previous", Previous)
            .Add("Next\n", Next);
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

    protected virtual string AppendToName(int index, T item)
    {
        return "";
    }
    protected virtual string PrependToName(int index, T item)
    {
        return "";
    }

    private void UpdatePage()
    {
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                    Console.WriteLine($"Page {_page + 1} / {GetPageCount()}\n{_resultCount} items found");
            }
        );
        for (var i = 0; i <  ItemsPerPage; i++)
        {
            var item = GetItem(i);
            ThisMenu.Items[i + 1].Name =  DisplayMenuName(i)  +
                              (ItemsPerPage - 1 == i ? "\n" : "");;

        }
    }

    public override void Run()
    {
        UpdatePage();
        base.Run();
    }
}
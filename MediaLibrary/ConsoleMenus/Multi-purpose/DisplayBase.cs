using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace MediaLibrary.ConsoleMenus.Multi_purpose;

public abstract class DisplayBase<T> : MenuBase where T : class
{
    protected const int PageSize = 5;
    protected int PageIndex;
    protected const string DisplayNameForEmptyItem = "---";

    protected abstract List<T> GetPageItems();
    protected abstract int GetTotalItemsCount();

    protected int GetPageCount(int totalItemCount)
    {
        return (int) Math.Ceiling(totalItemCount / (double) PageSize);
    }
    public int UpdatePageIndex(int totalPages)
    {
        if (PageIndex >= totalPages)
        {
            PageIndex = 0;
        }
        else if (PageIndex < 0)
        {
            PageIndex = Math.Max(totalPages - 1, 0);
        }

        return PageIndex;
    }

    public void Update()
    {
        var itemCount = GetTotalItemsCount();
        var totalPages = GetPageCount(itemCount);
        UpdatePageIndex(totalPages);
        var items = GetPageItems();
        
        ThisMenu.Configure(config =>
                {
                    config.WriteHeaderAction = () =>
                        Console.WriteLine($"Page {PageIndex + 1} / {totalPages}\n{itemCount} items found");
                }
            );
        for (var i = 0; i < PageSize; i++)
        {
            var item = i < items.Count ? items[i] : null;
            ThisMenu.Items[i + 1].Name = GetMenuDisplayName(item) + 
                (PageSize - 1 == i ? "\n" : "");
        }
    }

    public void Next()
    {
        ChangePage(1);
    }

    public void Previous()
    {
        ChangePage(-1);
    }

    private void ChangePage(int changeDirection)
    {
        PageIndex += changeDirection;
        Update();
    }
    
    protected DisplayBase(string title, int level) : base(title, level)
    {

        for (var i = 0; i < PageSize; i++)
            ThisMenu
                .Add("Item " + i, RunOnClick);

        ThisMenu
            .Add("Previous", Previous)
            .Add("Next\n", Next);
    }

    protected abstract string DisplayToMenu(T item);


    protected virtual string PrependToMenuDisplayName(T item)
    {
        return "";
    }
    protected virtual string AppendToMenuDisplayName(T item)
    {
        return "";
    }
    protected string GetMenuDisplayName(T? item)
    {
        if (item is null) return DisplayNameForEmptyItem;
        return PrependToMenuDisplayName(item) + DisplayToMenu(item) + AppendToMenuDisplayName(item);
    }

    protected abstract void RunOnClick(ConsoleMenu thisMenu);

    public override void Run()
    {
        Update();
        base.Run();
    }
}
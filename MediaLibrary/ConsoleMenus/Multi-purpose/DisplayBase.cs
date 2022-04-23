using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public abstract class DisplayBase<T> : MenuBase
{
    private const int ItemsPerPage = 5;

    protected readonly ItemIndexTracker<T> IndexTracker;

    protected DisplayBase(List<T> items, string title, int level) : base(title, level)
    {
        IndexTracker = new ItemIndexTracker<T>(items);
        for (var i = 0; i < ItemsPerPage; i++)
            ThisMenu
                .Add("Item " + i, RunOnClick);

        ThisMenu
            .Add("Previous", Previous)
            .Add("Next\n", Next);
    }

    protected abstract string DisplayToMenu(TrackerObject<T?> item);

    
    protected string DisplayMenuName(TrackerObject<T?> tracker)
    {
        if (tracker.Item is null) return "---";
        return PrependToName(tracker) + DisplayToMenu(tracker) + AppendToName(tracker) +
               (ItemsPerPage - 1 == tracker.LocalIndex ? "\n" : "");
        ;
        ;
    }

    protected abstract void RunOnClick(ConsoleMenu thisMenu);

    private void Next()
    {
        IndexTracker.Next();
        UpdatePage();
    }

    private void Previous()
    {
        IndexTracker.Previous();
        UpdatePage();
    }

    private int GetPageCount()
    {
        return Math.Max(1, (int) Math.Ceiling(IndexTracker.Items.Count / (double) ItemsPerPage));
    }

    protected virtual string AppendToName(TrackerObject<T?> localIndex)
    {
        return "";
    }

    protected virtual string PrependToName(TrackerObject<T?> localIndex)
    {
        return "";
    }

    protected void UpdatePage()
    {
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                    Console.WriteLine($"Page {IndexTracker.CurrentPage + 1} / {GetPageCount()}\n{IndexTracker.Items.Count} items found");
            }
        );
        for (var i = 0; i < ItemsPerPage; i++)
        {
            ThisMenu.Items[i + 1].Name = DisplayMenuName(IndexTracker.GetTrackerObject(i));
        }
    }

    public override void Run()
    {
        UpdatePage();
        base.Run();
    }
}
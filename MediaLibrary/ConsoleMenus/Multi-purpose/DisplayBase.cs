using Castle.Core.Internal;
using ConsoleApp1.FileAccessor;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public abstract class DisplayBase<T> : MenuBase
{
    protected readonly string EmptyItemString = "---";

    protected PageInfo<T> PageInfo = new(0, 5);

    protected DisplayBase(string title, int level) : base(title, level)
    {
        for (var i = 0; i < PageInfo.PageLength; i++)
            ThisMenu
                .Add("Item " + i, () => { });

        ThisMenu
            .Add("Previous", Previous)
            .Add("Next\n", Next);
    }

    protected abstract string DisplayToMenu(T? item);


    protected string DisplayMenuName(T? item)
    {
        if (item is null) return EmptyItemString;
        return PrependToName(item) + DisplayToMenu(item) + AppendToName(item);
    }

    protected abstract PageInfo<T> GetPageInfo(PageInfo<T> pageInfo);
    protected abstract void RunOnClick(ConsoleMenu thisMenu, T? item);

    private void Next()
    {
        PageInfo.NextPage();
        UpdatePage();
    }

    private void Previous()
    {
        PageInfo.PreviousPage();
        UpdatePage();
    }

    protected virtual string AppendToName(T? item)
    {
        return "";
    }

    protected virtual string PrependToName(T? item)
    {
        return "";
    }

    protected void UpdatePage()
    {
        GetPageInfo(PageInfo);
        ThisMenu.Configure(config =>
            {
                config.WriteHeaderAction = () =>
                    Console.WriteLine(
                        $"Page {PageInfo.PageIndex + 1} / {PageInfo.GetTotalPageCount}\n{PageInfo.TotalItemCount} items found");
            }
        );
        for (var i = 0; i < PageInfo.PageLength; i++)
        {
            var item = PageInfo.Items.IsNullOrEmpty() ? default : PageInfo.Items[i];
            ThisMenu.Items[i + 1].Name = DisplayMenuName(item) +
                                         (i == PageInfo.PageLength - 1 ? "\n" : "");
            var action = 
            ThisMenu.Items[i + 1].Action = () =>
            { 
                RunOnClick(ThisMenu, item);
            };;
        }
    }

    public override void Run()
    {
        UpdatePage();
        base.Run();
    }
}
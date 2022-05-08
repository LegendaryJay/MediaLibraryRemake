using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu.Add.EditGenre;

public class OccupationMenu : DisplayBase<Occupation>
{
    private readonly List<Occupation> _occupations;
    private readonly User _user;

    public OccupationMenu(User user, int level) : base("Choose Occupation", level)
    {
        _user = user;
        _occupations = FileIoSingleton.FileIo.GetAllOccupations();
        PageInfo.TotalItemCount = _occupations.Count;
    }
    protected override string DisplayToMenu(Occupation? item)
    {
        return item is null ? "" : item.Name;
    }

    protected override PageInfo<Occupation> GetPageInfo(PageInfo<Occupation> pageInfo)
    {
        pageInfo.Items =
            _occupations.Skip(pageInfo.PageIndex * pageInfo.PageLength).Take(pageInfo.PageLength).ToList() ??
            new List<Occupation>();
        return pageInfo;
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, Occupation? item)
    {
        if (item is null) return;
        _user.Occupation = item;
        ThisMenu.CloseMenu();
    }
}
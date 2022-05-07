using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;
using Microsoft.VisualBasic.CompilerServices;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu;

public class UserMenu : DisplayBase<User>
{
    public UserMenu(int level) : base("Users", level)
    {
    }

    protected override string DisplayToMenu(User? item)
    {
        return item is not null ? item.ToShortString() :"";
    }

    protected override PageInfo<User> GetPageInfo(PageInfo<User> pageInfo)
    {
        return PageInfo = FileIoSingleton.FileIo.GetPageUsers(pageInfo);
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, User? item)
    {
        throw new NotImplementedException();
    }
    
    
}
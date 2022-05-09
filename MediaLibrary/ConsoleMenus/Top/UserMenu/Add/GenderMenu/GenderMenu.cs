using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu.Add.GenderMenu;

public class GenderMenu : MenuBase
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly User _user;
    public GenderMenu(User user, int level) : base("Select Gender", level)
    {
        _user = user;
        ThisMenu.Add("Male", () => SetGender("M"));
        ThisMenu.Add("Female\n", () => SetGender("F"));
    }

    private void SetGender(string str)
    {
        _user.Gender = str;
        ThisMenu.CloseMenu();
        logger.Info("User set gender as " + str);
    }

}
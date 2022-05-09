using Castle.Core.Internal;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.UserMenu.Add;

public class AddUser : MenuBase
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly User _user = new User();

    private void SetAge()
    {
        var input = ReadLine.Read("Set Age: ");
        if (!int.TryParse(input, out var intInput)) return;
        if (intInput is > 0 and < 150) _user.Age = intInput;
        UpdatePage();
        logger.Info("User age set as " + input);
    }

    private void SetOccupation()
    {
        new OccupationMenu.OccupationMenu(_user, NextLevel()).Run();
        UpdatePage();
        logger.Info("User occupation set as " + _user.Occupation);
    }

    private void SetZip()
    {
        var input = ReadLine.Read("Set Zipcode: ");
        if (!int.TryParse(input, out var intInput)) return;
        if (intInput is > 9999 and < 99999) _user.ZipCode = input;
        UpdatePage();
        logger.Info("User zip set as " + input);
        
    }

    private bool ValidateUser()
    {
        return _user.Age > 0 && !_user.Gender.IsNullOrEmpty() && _user.Occupation is not null &&
               !_user.ZipCode.IsNullOrEmpty();
    }

    private void UpdatePage()
    {
        ThisMenu.Configure(config =>
        {
            config.WriteHeaderAction = () => { Console.WriteLine(_user.ToPrettyString()); };
        });
    }

    private void SaveAndExit()
    {
        if (!ValidateUser()) return;
        FileIoSingleton.FileIo.AddUser(_user);
        ThisMenu.CloseMenu();
        logger.Info("User Saved");
    }

    private void SetGender()
    {
        new GenderMenu.GenderMenu(_user, NextLevel()).Run();
        UpdatePage();
        logger.Info("User set gender as " + _user.Age);
    }

    public AddUser(int level) : base("Add User", level)
    {
        ThisMenu
            .Add("Set Age", SetAge)
            .Add("Set Gender", SetGender)
            .Add("Set ZipCode", SetZip)
            .Add("Set Occupation\n", SetOccupation)
            .Add("Save and Exit", SaveAndExit);
    }
}
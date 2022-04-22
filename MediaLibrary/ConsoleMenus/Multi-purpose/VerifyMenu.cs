namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class VerifyMenu : MenuBase
{
    private readonly Action _action;

    public VerifyMenu(string title, int level, Action action) : base(title, level)
    {
        _action = action;
        ThisMenu.Add("Yes",() =>  Choose(true));
        ThisMenu.Add("No",() => Choose(false));
    }

    public void Choose(bool choice)
    {
        if (choice) _action();
        ThisMenu.CloseMenu();
    }
}
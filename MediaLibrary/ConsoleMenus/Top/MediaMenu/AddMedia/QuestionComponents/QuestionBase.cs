using System.Reflection;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia.QuestionComponents;

public abstract class QuestionBase
{
    protected PropertyInfo Property;
    public string QuestionString { get; protected set; }
    public bool IsList { get; protected set; }

    public abstract void SetValue(Movie mediaTarget, string input);
}
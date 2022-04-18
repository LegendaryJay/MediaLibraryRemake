using System.Linq.Expressions;
using System.Reflection;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia.QuestionComponents;

public class QuestionList<T> : QuestionBase

{
    private readonly List<T> _returnList;

    public QuestionList(Expression<Func<Movie, List<T>>> property)
    {
        Property = (PropertyInfo) ((MemberExpression) property.Body).Member;
        QuestionString = $"Movie | Input List of {Property.Name}:";
        _returnList = new List<T>();
        IsList = true;
    }

    public override void SetValue(Movie mediaTarget, string input)
    {
        _returnList.Add(
            TypeConvert.Convert<string, T>(input)
        );
        Property.SetValue(mediaTarget, _returnList, null);
    }
}
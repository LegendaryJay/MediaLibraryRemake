using System.Linq.Expressions;
using System.Reflection;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia.QuestionComponents;

public class Question<T> : QuestionBase
{
    public Question(Expression<Func<Movie, T>> property)
    {
        Property = (PropertyInfo) ((MemberExpression) property.Body).Member;
        QuestionString = $"Movie | Input {Property.Name}:";
        IsList = false;
    }


    public override void SetValue(Movie mediaTarget, string input)
    {
        Property.SetValue(
            mediaTarget,
            TypeConvert.Convert<string, T>(input)
        );
    }
}
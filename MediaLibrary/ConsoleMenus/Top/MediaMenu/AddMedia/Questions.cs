using ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia.QuestionComponents;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using static ConsoleApp1.ConsoleMenus.Tools.ManualInputTools;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia;

public class Questions : IQuestions
{
    private readonly List<QuestionBase> _questionList;

    public Questions(List<QuestionBase> questions)
    {
        _questionList = questions;
    }


    public void Ask()
    {
        Movie media = new();
        foreach (var question in _questionList)
        {
            Console.WriteLine(
                question.QuestionString
                + (
                    question.IsList ? $"\n\t Enter \"{CancelKey}\" to Exit" : ""
                )
            );

            do
            {
                var input = Input();
                if (IsExit(input)) break;

                question.SetValue(media, input);
            } while (question.IsList);
        }

        Console.WriteLine("Writing to file");
        Console.WriteLine(media.ToPrettyString());
        FileIoSingleton.Instance.FileIo.AddMovie(media);
    }
}
using ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia.QuestionComponents;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia;

public static class QuestionsFactory
{
    public static IQuestions GetQuestions()
    {
        return new Questions(new List<QuestionBase>
        {
            new Question<string>(m => m.Title)
        });
    }
}
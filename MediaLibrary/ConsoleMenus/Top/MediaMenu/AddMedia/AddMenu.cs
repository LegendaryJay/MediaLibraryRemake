

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia;

public class AddMenu
{
    private readonly IQuestions _questions;

    public AddMenu()
    {
        _questions = QuestionsFactory.GetQuestions();
    }

    public void Run()
    {
        try
        {
            _questions.Ask();
        }
        catch (Exception)
        {
            Console.WriteLine("Something went wrong");
        }
        finally
        {
            Console.ReadLine();
        }
    }
}
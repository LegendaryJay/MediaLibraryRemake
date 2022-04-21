using ConsoleApp1.ConsoleMenus.Multi_purpose;

namespace ConsoleApp1.ConsoleMenus.Top.MediaMenu.AddMedia.QuestionComponents;

public class MultipleChoiceMenu : MenuBase
{
    public readonly List<int> Output = new();
    private readonly List<bool> chosen = new();

    protected MultipleChoiceMenu(List<string> choices, string title, int level) : base(title, level)
    {
        foreach (var choice in choices)
        {
            chosen.Add(false);
            ThisMenu.Add(choice, thisMenu =>
            {
                var index = ThisMenu.CurrentItem.Index - 1;
                ToggleChosen(index);
                thisMenu.CurrentItem.Name = (IsChosen(index) ? " * " : "") + choices[index];
            });
            ThisMenu.Add("Submit", thisMenu =>
            {
                for (var i = 0; i < choices.Count; i++)
                    if (chosen[i])
                        Output.Add(i);

                thisMenu.CloseMenu();
            });
        }
    }

    private void ToggleChosen(int index)
    {
        chosen[index] = !chosen[index];
    }

    private bool IsChosen(int index)
    {
        return chosen[index];
    }
}
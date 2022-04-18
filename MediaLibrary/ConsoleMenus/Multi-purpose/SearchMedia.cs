using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using static ConsoleApp1.ConsoleMenus.Tools.ManualInputTools;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class SearchMedia
{
    private readonly int _level;

    public SearchMedia(int level)
    {
        _level = level;
    }
    public void Run()
    {
        if (!Ask("Title Search", out var input)) return;

        new DisplayMenu(FileIoSingleton.Instance.FileIo.FilterMovieByTitle(input), x => x.ToPrettyString(), $"Results for \"{input}\"",
            _level).Run();
    }
}
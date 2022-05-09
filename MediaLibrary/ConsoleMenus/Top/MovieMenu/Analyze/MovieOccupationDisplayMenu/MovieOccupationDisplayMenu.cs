using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieOccupationDisplayMenu;

public class MovieOccupationDisplayMenu : DisplayBase<MovieWithOccupation>
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    public MovieOccupationDisplayMenu(int level) : base("Top Movies by Occupation", level)
    {
        _items = FileIoSingleton.FileIo.BestMovieByOccupation();
    }

    private readonly List<MovieWithOccupation> _items;

    protected override string DisplayToMenu(MovieWithOccupation? item)
    {
        return item is not null ? item.ToPrettyString() : EmptyItemString;
    }

    protected override PageInfo<MovieWithOccupation> GetPageInfo(PageInfo<MovieWithOccupation> pageInfo)
    {
        pageInfo.TotalItemCount = _items.Count;
        pageInfo.Items =
            _items.Skip(pageInfo.PageIndex * pageInfo.PageLength).Take(pageInfo.PageLength).ToList() ??
            new List<MovieWithOccupation>();
        return pageInfo;
        
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, MovieWithOccupation? item)
    {
        logger.Info("User did nothing in MovieOccupationDisplayMenu");
    }
}
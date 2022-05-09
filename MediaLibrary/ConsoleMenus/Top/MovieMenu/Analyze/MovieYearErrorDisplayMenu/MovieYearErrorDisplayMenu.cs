using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.Analyze.MovieYearErrorDisplayMenu;

public class MovieYearErrorDisplayMenu : DisplayBase<MovieWithYearError>
{
    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    private List<MovieWithYearError> _items; 
    public MovieYearErrorDisplayMenu(int level) : base("Movie Year Difference", level)
    {
        _items = FileIoSingleton.FileIo.GetMovieDiff();
    }

    protected override string DisplayToMenu(MovieWithYearError? item)
    {
        return item is not null
            ? $"{item.Movie.Title,-70} Release Year: {item.Movie.ReleaseDate.Year} | Difference: {item.YearDIff}"
            : EmptyItemString;
    }

    protected override PageInfo<MovieWithYearError> GetPageInfo(PageInfo<MovieWithYearError> pageInfo)
    {
        pageInfo.TotalItemCount = _items.Count;
        pageInfo.Items =
            _items.Skip(pageInfo.PageIndex * pageInfo.PageLength).Take(pageInfo.PageLength).ToList() ??
            new List<MovieWithYearError>();
        return pageInfo;
    }

    protected override void RunOnClick(ConsoleMenu thisMenu, MovieWithYearError? item)
    {
        logger.Info("user did nothing in MovieYearErrorMenu");
    }
}
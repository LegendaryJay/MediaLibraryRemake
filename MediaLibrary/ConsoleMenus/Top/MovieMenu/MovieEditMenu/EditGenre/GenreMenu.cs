using System.Collections.Specialized;
using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;
using Microsoft.EntityFrameworkCore.Storage;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;

public class GenreMenu : DisplayBase<GenreDummy>
{
    private const string SelectedString = " * ";
    private readonly Movie _movie;
    private List<Genre> _output = new();


    public GenreMenu(Movie? movie) : base(
        FileIoSingleton.Instance.FileIo.GetAllGenres().Select(x => new GenreDummy(x, false)).ToList(), "Choose Genres", 2)
    {
        _movie = movie ?? new Movie();
        _movie.Genres ??= new List<Genre>();

        foreach (
            var genreDummy in IndexTracker.Items.Where(x => _movie.Genres.Any(g => g.Id == x.Id))
        ) genreDummy.IsRecorded = true;

        ThisMenu.Add("Save Changes", thisMenu =>
        {
            SaveResult();
            thisMenu.CloseMenu();
        });
    }

    private void SaveResult()
    {
        _output = IndexTracker.Items
            .Where(x => x.IsRecorded)
            .Select(x => x.Genre)
            .ToList();
    }

    protected override string PrependToName(TrackerObject<GenreDummy?> tracker)
    {
        return tracker.Item!.IsRecorded ? SelectedString : "";
    }


    private void ToggleGenre(TrackerObject<GenreDummy?> tracker)
    {
        if (!tracker.isValid) return;
        tracker.Item!.IsRecorded = !tracker.Item.IsRecorded;
    }

    protected override string DisplayToMenu(TrackerObject<GenreDummy?> tracker)
    {
        return tracker.Item?.Name ?? "";
    }

    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        var tracker = IndexTracker.GetTrackerObject(thisMenu.CurrentItem.Index - 1);
        if (!tracker.isValid) return;
        ToggleGenre(tracker);
        thisMenu.CurrentItem.Name = DisplayMenuName(tracker);
    }

    public void Run(out List<Genre> genres)
    {
        base.Run();
        genres = _output;
    }
}
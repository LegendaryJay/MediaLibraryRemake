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
    private List<Genre> _output = new();



    public GenreMenu(Movie? movie) : base(
        
        FileIoSingleton.FileIo.GetAllGenres().Select(x => new GenreDummy(x, false)).ToList(), "Choose Genres", 2)
    {
        movie ??= new Movie();
        movie.MovieGenres ??= new List<MovieGenres>();
        var genreList = movie.MovieGenres.Select(x => x.Genre).ToList();

        foreach (
            var genreDummy in IndexTracker.Items.Where(x => genreList.Any(g => g.Id == x.Id))
        ) genreDummy.IsRecorded = true;

        ThisMenu.Add("Save Changes", thisMenu =>
        {
            SaveResult();
            thisMenu.CloseMenu();
        });
    }

    public GenreMenu() : this(new Movie())
    {
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
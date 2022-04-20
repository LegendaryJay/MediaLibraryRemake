using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;

public class GenreMenu : DisplayBase<Genre>
{
    private const string SelectedString = " * ";
    private readonly List<bool> _activeGenres;
    private List<Genre> _result;

    private void SaveResult()
    {
        _result = Enumerable.Range(0, _activeGenres.Count)
            .Where(i => _activeGenres[i])
            .Select(x => Items[x]  ?? throw new Exception("Null Genre"))
            .Distinct()
            .ToList();
    }

    public GenreMenu(Movie? movie) : base(FileIoSingleton.Instance.FileIo.GetAllGenres(), "Choose Genres", 2)
    {
        _result = Items;
        _activeGenres = new List<bool>(new bool[Items.Count]);
        if (movie?.MovieGenres != null && movie.MovieGenres.Count > 0)
        {
            foreach (var genre in movie.MovieGenres.ToList())
            {
                _activeGenres[Items.FindIndex(x => x.Id == genre.Genre.Id)] = true;
            }
        }

        ThisMenu.Add("Save Changes", thisMenu =>
        {
            SaveResult();
            thisMenu.CloseMenu();
        });
    }

    protected override string PrependToName(int index, Genre item)
    {
        return _activeGenres[GetItemIndex(index)] ? SelectedString : "";
    }


    private void ToggleGenre(int index)
    {
        _activeGenres[index] = !_activeGenres[index];
    }

    protected override string DisplayToMenu(Genre item)
    {
        return item.Name;
    }

    protected override void RunOnClick(ConsoleMenu thisMenu)
    {
        var index = thisMenu.CurrentItem.Index - 1;
        if (GetItemIndex(index) >= Items.Count) return;
        ToggleGenre(GetItemIndex(index));
        thisMenu.CurrentItem.Name = DisplayMenuName(index);

    }

    public void Run(out List<Genre> selectedGenres)
    {
        base.Run();
        selectedGenres = _result;
    }
}
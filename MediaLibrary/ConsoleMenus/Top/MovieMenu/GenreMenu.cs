using ConsoleApp1.ConsoleMenus.Multi_purpose;
using ConsoleApp1.FileAccessor;
using ConsoleApp1.MediaEntities;
using ConsoleTools;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu;

public class GenreMenu : DisplayBase<Genre>
{
    private const string SelectedString = " * ";
    private readonly List<bool> _activeGenres;
    public GenreMenu(Movie? movie) : base(FileIoSingleton.Instance.FileIo.GetAllGenres(), "Choose Genres", 2)
    {
        _activeGenres = new List<bool>(new bool[_items.Count]);
        var hasGenres = movie.MovieGenres is not null && movie.MovieGenres.Count > 0;
        if (hasGenres)
        {
            foreach (var genre in movie.MovieGenres.ToList())
            {
                _activeGenres[_items.FindIndex(x => x.Id == genre.Genre.Id)] = true;
            }
        }
        
        ThisMenu.Add("Save Changes", () => { });
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
        ToggleGenre(GetItemIndex(index));
        thisMenu.CurrentItem.Name = DisplayMenuName(index);
    }
}
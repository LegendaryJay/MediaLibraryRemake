using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;

public class GenreDummy
{
    public Genre Genre { get; }
    public long Id { get => Genre.Id; }
    public string Name { get => Genre.Name; }
    public bool IsRecorded { get; set; }
    
    


    public GenreDummy(Genre genre, bool isRecorded)
    {
        Genre = genre;
        IsRecorded = isRecorded;
    }
}
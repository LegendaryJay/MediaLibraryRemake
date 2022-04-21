namespace ConsoleApp1.MediaEntities;

public class Genre
{
    public long Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Movie> Movies { get; set; }
}
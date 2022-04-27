namespace ConsoleApp1.MediaEntities;

public class Genre
{
    public long Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<MovieGenres> MovieGenres { get; set; }


}
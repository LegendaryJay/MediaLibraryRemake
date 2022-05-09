using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.MediaEntities;

public class Genre
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<MovieGenres> MovieGenres { get; set; }


}
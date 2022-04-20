using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.MediaEntities;

public class MovieGenre
{
    [Key]
    public int Id { get; set; }
    public virtual Movie Movie { get; set; }
    public virtual Genre Genre { get; set; }
}
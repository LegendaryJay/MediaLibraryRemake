using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.MediaEntities;

public class UserMovie
{
    [Key] public long Id { get; set; }
    public long UserId { get; set; }
    public long MovieId { get; set; }


    public long Rating { get; set; }
    public DateTime RatedAt { get; set; }

    public virtual User User { get; set; }
    public virtual Movie Movie { get; set; }
}
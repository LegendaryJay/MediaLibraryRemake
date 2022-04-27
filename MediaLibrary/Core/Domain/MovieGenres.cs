

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.MediaEntities
{
    public class MovieGenres
    {
        [Key]
        public int Id {get;set;}
        public long MovieId {get;set;}
        public long GenreId {get;set;}
        public virtual Movie Movie { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
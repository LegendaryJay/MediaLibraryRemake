using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.MediaEntities;

public class User
{
    [Key] public long Id { get; set; }

    public long Age { get; set; }
    public string Gender { get; set; }
    public string ZipCode { get; set; }

    public virtual Occupation Occupation { get; set; }
    public virtual ICollection<UserMovie> UserMovies { get; set; }
    
    public string ToShortString()
    {
        return $"ID: {Id, -4}|  {Age,-2}/{Gender,-1}";
    }

    public string ToPrettyString()
    {
        return $" - User {Id}:" +
               $"\n\tGender: {Gender}" +
               $"\n\tAge: {Age}" +
               $"\n\tZipCode {ZipCode}" +
               $"\n\tOccupation {Occupation}";
        
    }
}

//Looks like there are 2 main groups: 
//Movie: Title, release date, List(Genre), List(Ratings, DateRated)
//User: Id, age, gender, zipcode, Occupations

//Users rate
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp1.MediaEntities;

public class Occupation
{
    [Key] public long Id { get; set; }

    public string Name { get; set; }
}
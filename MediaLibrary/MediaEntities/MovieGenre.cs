﻿namespace ConsoleApp1.MediaEntities;

public class MovieGenre
{
    public int Id { get; set; }
    public virtual Movie Movie { get; set; }
    public virtual Genre Genre { get; set; }
}
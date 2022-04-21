// @nuget: EntityFramework
// @nuget: EntityFramework.SqlServerCompact
// @nuget: Microsoft.SqlServer.Compact
// @nuget: Z.EntityFramework.Extensions


using ConsoleApp1.MediaEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1.FileAccessor.Database.Context;

public class MovieContext : DbContext
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Occupation> Occupations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("MovieContext")!);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Movie>()
            .HasMany(p => p.Genres)
            .WithMany(p => p.Movies)
            .UsingEntity<MovieGenres>(
                j => j
                    .HasOne(pt => pt.Genre)
                    .WithMany()
                    .HasForeignKey(pt => pt.GenreId),
                j => j
                    .HasOne(pt => pt.Movie)
                    .WithMany()
                    .HasForeignKey(pt => pt.MovieId)
                );
    }
    
}
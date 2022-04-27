// using Castle.Core.Internal;
// using ConsoleApp1.ConsoleMenus.Multi_purpose;
// using ConsoleApp1.ConsoleMenus.Top.MovieMenu.MovieEditMenu.EditGenre;
// using ConsoleApp1.MediaEntities;
// using ConsoleTools;
//
// namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.FilterMenu;
//
// public class FilterMenu : MenuBase
// {
//     private readonly ItemIndexTracker<Movie> _indexTracker;
//
//     private const string FilterPrefix = " :";
//     private const string FilterSeparator = ", ";
//     private readonly string _lastTitle;
//     private readonly List<Movie> _movies;
//
//     private void FilterByTitle()
//     {
//         var filterBy = ReadLine.Read("What key words do you want to filter By?");
//         if (filterBy is not null)
//         {
//             var filterByList = filterBy.Split(null);
//             var newMovies = _movies.Where(m => filterByList.All(searchTerm => m.Title.ToLower().Contains(searchTerm.ToLower()))).ToList();
//             if (newMovies.Any())
//             {
//                 AddMovieMenuLayer($"\"{filterBy}\"", newMovies);
//                 return;
//             }
//         }
//
//         FailedResponse();
//     }
//
//     private void FilterByYear()
//     {
//         var filterBy = ReadLine.Read("What year do you want to filter By?");
//         if (filterBy is not null && filterBy.Length == 4 && int.TryParse(filterBy, out var yearInt))
//         {
//             var newMovies = _movies.Where(m => m.ReleaseDate.Year == yearInt || m.Title.Contains($"({yearInt})"))
//                 .ToList();
//             if (newMovies.Any())
//             {
//                 AddMovieMenuLayer($"From {filterBy}", newMovies);
//                 return;
//             }
//         }
//
//         FailedResponse();
//     }
//
//     private void FilterByGenres()
//     {
//         new GenreMenu().Run(out var filterByList);
//         if (!filterByList.IsNullOrEmpty())
//         {
//             var filterByIdList = filterByList.Select(x => x.Id);
//             var newMovies = _movies.Where(m =>
//                 filterByIdList.All(filterId =>
//                     m.MovieGenres
//                         .Select(
//                             mg => mg.GenreId)
//                         .Contains(filterId)
//                 )
//             ).ToList();
//             if (newMovies.Any())
//             {
//                 
//                 AddMovieMenuLayer($"Genres {string.Join(", ", filterByList.Select(x => x.Name))}", newMovies);
//                 return;
//             }
//         }
//
//         FailedResponse();
//     }
//
//     private static void FailedResponse()
//     {
//         ReadLine.Read("Cannot Find anything like that");
//     }
//
//
//     private void AddMovieMenuLayer(string filterByString, List<Movie> newMovies)
//     {
//         var newTitle = _lastTitle + (_lastTitle.Contains(FilterPrefix) ? FilterSeparator : FilterPrefix) +
//                        filterByString;
//         ThisMenu.CloseMenu();
//         new MovieMenu(newMovies, newTitle, Level).Run();
//     }
//
//
//     public FilterMenu(ItemIndexTracker<Movie> indexTracker, string lastTitle, int level) : base("Filter Menu",
//         level)
//     {
//         _lastTitle = lastTitle;
//         _movies = new List<Movie>(indexTracker.Items);
//
//         _indexTracker = new ItemIndexTracker<Movie>(new List<Movie>(indexTracker.Items));
//         const string prefix = "Filter by ";
//         ThisMenu.Add(prefix + "Title", FilterByTitle)
//             .Add(prefix + "Year", FilterByYear)
//             .Add(prefix + "Genre", FilterByGenres);
//     }
// }
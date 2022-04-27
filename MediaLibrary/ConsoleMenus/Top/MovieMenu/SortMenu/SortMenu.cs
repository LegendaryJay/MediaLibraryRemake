// using Castle.Core.Internal;
// using ConsoleApp1.ConsoleMenus.Multi_purpose;
// using ConsoleApp1.MediaEntities;
//
// namespace ConsoleApp1.ConsoleMenus.Top.MovieMenu.SortMenu;
//
// public class SortMenu : MenuBase
// {
//     private readonly ItemIndexTracker<Movie> _tracker;
//
//     
//     private void OnPress<T>(Func<Movie, T?> toComparable, T defaultValue) where T : IComparable
//     {
//         _tracker.Items.Sort((x, y) => (toComparable(x) ?? defaultValue).CompareTo(toComparable(y) ?? defaultValue));
//         ThisMenu.CloseMenu();
//     }
//
//     public SortMenu(ItemIndexTracker<Movie> tracker, int level) : base("Sort By", level)
//     {
//         _tracker = tracker;
//         ThisMenu
//             .Add("Id", () => OnPress(x => x.Id, 0))
//             .Add("Title", () => OnPress(x => x.Title, ""))
//             .Add("ReleaseDate", () => OnPress(x => x.ReleaseDate,DateTime.MinValue))
//             .Add("Rating", () => OnPress(x => x.UserMovies.IsNullOrEmpty() ? 0 :  -1 * x.UserMovies.Average(y => y.Rating), 0));
//     }
// }
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.FileAccessor;

public static class EnumerableTools
{
    public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> source,
        Func<T, object> keySelector, ListSortDirection sortDirection)
    {
        return sortDirection == ListSortDirection.Ascending
            ? source.OrderBy(keySelector)
            : source.OrderByDescending(keySelector);
    }
    
}
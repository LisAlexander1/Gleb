using System.Collections.ObjectModel;

namespace Gleb.Helpers;

public static class ListExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T>? list)
    {
        return list == null ? [] : new ObservableCollection<T>(list);
    }
    
}
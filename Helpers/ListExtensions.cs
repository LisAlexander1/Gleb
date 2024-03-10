using System.Collections.ObjectModel;

namespace Gleb.Helpers;

public static class ListExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list)
    {
        return new ObservableCollection<T>(list);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static bool ContainsOnly<T>(this List<T> list, IEnumerable<T> objects)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));
        else if (objects == null)
            throw new ArgumentNullException(nameof(objects));

        if (objects.Count() == 0 || objects.Count() != list.Count)
            return false;

        foreach (var obj in objects)
        {
            if (!list.Contains(obj))
                return false;
        }

        return true;
    }
}

public static class GameExtensions
{
    public static IChemical GetFirstElementWithName(this Game g, string name)
    {
        return g.ActiveElements.First(e => e.Name == name);
    }
}
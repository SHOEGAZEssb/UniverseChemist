using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Extensions for a <see cref="List{T}"/>.
/// </summary>
public static class ListExtensions
{
  /// <summary>
  /// Check if the <paramref name="list"/> contains only
  /// the given <paramref name="objects"/> (in any order).
  /// </summary>
  /// <typeparam name="T">Type of the object in the list.</typeparam>
  /// <param name="list">List to check.</param>
  /// <param name="objects">Objects to check.</param>
  /// <returns>True if the <paramref name="list"/> contains the <paramref name="objects"/>.</returns>
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
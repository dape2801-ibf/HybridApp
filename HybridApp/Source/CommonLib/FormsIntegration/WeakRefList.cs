using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.FormsIntegration;
/// <summary>
/// Represents a list of weak references.
/// </summary>
internal class WeakRefList<T> : IEnumerable<T> where T : class
{
    private readonly List<WeakReference<T>> references = [];

    /// <summary>
    /// Returns the enumerator.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in references.ToList())
        {
            if (item.TryGetTarget(out var tmp))
            {
                yield return tmp;
            }
            else
            {
                references.Remove(item);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Clears the list.
    /// </summary>
    public void Clear()
    {
        references.Clear();
    }

    /// <summary>
    /// Adds an item to the list.
    /// </summary>
    public void Add(T item)
    {
        references.Add(new WeakReference<T>(item));
    }
}

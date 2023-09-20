using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

[Serializable]
public class ItemCollection<T> : List<T>
{
    public UnityEvent<int, T> OnItemAdded = new();
    public UnityEvent<T> OnItemRemoved = new();
    
    public List<T> this[Range range]
    {
        get {
            return GetRange(range.Start.Value, range.End.Value);
        }
    }
    public int[] this[T item]
    {
        get{
            List<int> indices = new();
            for(int i = 0; i < Count; i++)
            {
                if(this[i].Equals(item))
                    indices.Add(i);
            }
            return indices.ToArray();
        }
    }
    public int Max = -1;

    /// <summary>
    /// Adds the given Item to the ItemCollection.
    /// Invokes the OnItemAdded event for the Item being added
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if the Item could fit in the Collection</returns>
    public new bool Add(T item)
    {
        if(Count < Max || Max == -1)
        {
            Add(item);
            OnItemAdded.Invoke(Count - 1, item);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds the given Items to the ItemCollection.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="items"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public new bool AddRange(IEnumerable<T> items)
    {
        if(Count + items.Count() < Max || Max == -1)
        {
            for(int i = 0; i < items.Count(); i++)
            {
                OnItemAdded.Invoke(Count + i, items.ElementAt(i));
            }
            AddRange(items);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds the given Item n times to the ItemCollection.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool AddN(T item, int count)
    {
        if(Count + count < Max || Max == -1)
        {
            for(int i = 0; i < count; i++)
            {
                Add(item);
                OnItemAdded.Invoke(Count - 1, item);
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Inserts the given Item at the given Index.
    /// Invokes the OnItemAdded event for the Item being added
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <returns>true if the Item could fit in the Collection</returns>
    public new bool Insert(int index, T item)
    {
        if(Count + 1 < Max || Max == -1)
        {
            base.Insert(index, item);
            OnItemAdded.Invoke(index, item);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Inserts the given Items at the given Index.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="index"></param>
    /// <param name="items"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public new bool InsertRange(int index, IEnumerable<T> items)
    {
        if(Count + items.Count() < Max || Max == -1)
        {
            base.InsertRange(index, items);
            for(int i = 0; i < items.Count(); i++)
            {
                OnItemAdded.Invoke(index + i, items.ElementAt(i));
            }
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Replaces the Item at the given Index with the given Item
    /// Invokes the OnItemRemoved event for the Item being replaced
    /// Invokes the OnItemAdded event for the Item being added
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    /// <returns>true if the Item could fit in the Collection</returns>
    public bool Replace(int index, T item)
    {
        if(index < Max || Max == -1)
        {
            if(Count > index)
                OnItemRemoved?.Invoke(this[index]);
                
            if(Count <= index)
                base.Insert(index, item);
            else
            {
                this[index] = item;
            }
            OnItemAdded?.Invoke(index, item);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Replaces the Items at the given Index with the given Items.
    /// Invokes the OnItemRemoved event for each Item being replaced
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="index"></param>
    /// <param name="items"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool ReplaceRange(int index, IEnumerable<T> items)
    {
        var replaced = false;
        for(int i = 0; i < items.Count(); i++)
        {
            if(index + i < Max || Max == -1)
            {
                Replace(index + i, items.ElementAt(i));
                replaced = true;
            }
        }
        return replaced;
    }

    /// <summary>
    /// Removes the Item from the ItemCollection, Invoking the OnItemRemoved event.
    /// </summary>
    /// <param name="item"></param>
    public new void Remove(T item)
    {
        OnItemRemoved.Invoke(item);
        base.Remove(item);
    }

    /// <summary>
    /// Removes the Item at the given Index from the ItemCollection.
    /// </summary>
    /// <param name="index"></param>
    public new void RemoveAt(int index)
    {
        Remove(this[index]);
    }

    /// <summary>
    /// Removes the given Range of Items from the ItemCollection.
    /// </summary>
    /// <param name="range"></param>
    public void RemoveRange(Range range)
    {
        Array.ForEach(this[range].ToArray(), x => Remove(x));
    }

    /// <summary>
    /// Removes all of the specified Items from the ItemCollection.
    /// </summary>
    /// <param name="predicate"></param>
    public new void RemoveAll(Predicate<T> predicate)
    {
        Array.ForEach(FindAll(predicate).ToArray(), x => Remove(x));
    }

    /// <summary>
    /// Removes all of the specified Items from the ItemCollection.
    /// </summary>
    /// <param name="items"></param>
    public void RemoveAll(IEnumerable<T> items)
    {
        Array.ForEach(items.ToArray(), x => Remove(x));
    }

    /// <summary>
    /// Removes the first n of the specified Item from the ItemCollection.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="n"></param>
    public void RemoveCount(T item, int n)
    {
        for(int i = 0; i < n; i++)
        {
            base.Remove(item);
        }
    }

    /// <summary>
    /// Removes all Items from the ItemCollection.
    /// </summary>
    public new void Clear()
    {
        ForEach(x => Remove(x));
    }

    /// <summary>
    /// Transforms all Items from another ItemCollection into this one.
    /// </summary>
    /// <param name="other"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool TransferAllFrom(ItemCollection<T> other)
    {
        if(Count + other.Count < Max || Max == -1)
        {
            AddRange(other);
            other.Clear();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the Item from the other ItemCollection, by Index, and adds it to this one.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="index"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool TransferFromOtherByIndex(ItemCollection<T> other, int index)
    {
        if(Count < Max || Max == -1)
        {
            Add(other[index]);
            other.RemoveAt(index);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the Item from the other ItemCollection and adds it to this one.
    /// If n is greater than 1, it will remove the first n items from the other ItemCollection.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="other"></param>
    /// <param name="item"></param>
    /// <param name="n"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool TransferItemFromOther(ItemCollection<T> other, T item, int n = 1)
    {
        if(Count + n < Max || Max == -1)
        {
            var others = other.FindAll(x => x.Equals(item));
            for(int i = 0; i < n; i++)
            {
                if(i < others.Count && Count < Max || Max == -1)
                {
                    var first = other.First(x => x.Equals(item));
                    if(first != null)
                    {
                        other.Remove(first);
                        Add(first);
                    }
                }
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the Items from the other ItemCollection and adds them to this one.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="other"></param>
    /// <param name="items"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool TransferItemsFromOther(ItemCollection<T> other, IEnumerable<T> items)
    {
        if(Count + items.Count() < Max || Max == -1)
        {
            AddRange(other.FindAll(x => items.Contains(x)));
            other.RemoveAll(x => items.Contains(x));
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the Items from the other ItemCollection and adds them to this one.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="other"></param>
    /// <param name="predicate"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool TransferItemsFromOther(ItemCollection<T> other, Predicate<T> predicate)
    {
        var items = other.FindAll(predicate);
        if(Count + items.Count() < Max || Max == -1)
        {
            AddRange(items);
            other.RemoveAll(items);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Removes the Items from the other ItemCollection and adds them to this one.
    /// Invokes the OnItemAdded event for each Item being added
    /// </summary>
    /// <param name="other"></param>
    /// <param name="range"></param>
    /// <returns>true if the Items could fit in the Collection</returns>
    public bool TransferRangeFromOther(ItemCollection<T> other, Range range)
    {
        if(Count + range.End.Value - range.Start.Value < Max || Max == -1)
        {
            AddRange(other[range]);
            other.RemoveRange(range);
            return true;
        }
        return false;
    }
}
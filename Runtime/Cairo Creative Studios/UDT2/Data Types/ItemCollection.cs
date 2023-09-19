using System;
using System.Collections.Generic;
using System.Linq;
using Rich.MultipurposeEvents;

namespace Rich.DataTypes
{
    [Serializable]
    public class ItemCollection<T>
    {
        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }
        public List<T> this[Range range]
        {
            get {
                return list.GetRange(range.Start.Value, range.End.Value);
            }
        }
        public int[] this[T item]
        {
            get{
                List<int> indices = new();
                for(int i = 0; i < list.Count; i++)
                {
                    if(list[i].Equals(item))
                        indices.Add(i);
                }
                return indices.ToArray();
            }
        }
        public int Count => list.Count;
        public int Capacity => list.Capacity;
        public int Max = -1;
        public MultipurposeEvent<int, T> OnItemAdded = new();
        public MultipurposeEvent<T> OnItemRemoved = new();
        private List<T> list = new();

        /// <summary>
        /// Adds the given Item to the ItemCollection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if the Item could fit in the Collection</returns>
        public bool Add(T item)
        {
            if(Count < Max || Max == -1)
            {
                list.Add(item);
                OnItemAdded.Invoke(Count - 1, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the given Items to the ItemCollection.
        /// </summary>
        /// <param name="items"></param>
        /// <returns>true if the Items could fit in the Collection</returns>
        public bool AddRange(IEnumerable<T> items)
        {
            if(Count + items.Count() < Max || Max == -1)
            {
                for(int i = 0; i < items.Count(); i++)
                {
                    OnItemAdded.Invoke(Count + i, items.ElementAt(i));
                }
                list.AddRange(items);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the given Item n times to the ItemCollection.
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
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns>true if the Item could fit in the Collection</returns>
        public bool Insert(int index, T item)
        {
            if(list.Count + 1 < Max || Max == -1)
            {
                OnItemRemoved.Invoke(list[index]);
                list.Insert(index, item);
                OnItemAdded.Invoke(index, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Inserts the given Items at the given Index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="items"></param>
        /// <returns>true if the Items could fit in the Collection</returns>
        public bool InsertRange(int index, IEnumerable<T> items)
        {
            if(list.Count + items.Count() < Max || Max == -1)
            {
                Array.ForEach(items.ToArray(), x => OnItemRemoved.Invoke(x));
                list.InsertRange(index, items);
                for(int i = 0; i < items.Count(); i++)
                {
                    OnItemAdded.Invoke(index + i, items.ElementAt(i));
                }
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Replaces the Item at the given Index with the given Item.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns>true if the Item could fit in the Collection</returns>
        public bool Replace(int index, T item)
        {
            if(index < Max || Max == -1)
            {
                OnItemRemoved.Invoke(list[index]);
                //list.InsertRange(index, items);
                if(list.Count < index)
                    list.Insert(index, item);
                else
                {
                    list[index] = item;
                    OnItemAdded.Invoke(index, item);
                }
                OnItemAdded.Invoke(index, item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Replaces the Items at the given Index with the given Items.
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
                    if(list.Count < i)
                    {
                        OnItemRemoved.Invoke(items.ElementAt(i));
                        list[i] = items.ElementAt(i);
                        OnItemAdded.Invoke(index + i, items.ElementAt(i));
                    }
                    else
                    {
                        list.Insert(i, items.ElementAt(i));
                        OnItemAdded.Invoke(index + i, items.ElementAt(i));
                    }
                    replaced = true;
                }
            }
            return replaced;
        }

        /// <summary>
        /// Removes the Item from the ItemCollection.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            OnItemRemoved.Invoke(item);
            list.Remove(item);
        }

        /// <summary>
        /// Removes the Item at the given Index from the ItemCollection.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            OnItemRemoved.Invoke(list[index]);
            list.RemoveAt(index);
        }

        /// <summary>
        /// Removes the given Range of Items from the ItemCollection.
        /// </summary>
        /// <param name="range"></param>
        public void RemoveRange(Range range)
        {
            Array.ForEach(this[range].ToArray(), x => OnItemRemoved.Invoke(x));
            list.RemoveRange(range.Start.Value, range.End.Value);
        }

        /// <summary>
        /// Removes all of the specified Items from the ItemCollection.
        /// </summary>
        /// <param name="predicate"></param>
        public void RemoveAll(Predicate<T> predicate)
        {
            Array.ForEach(list.FindAll(predicate).ToArray(), x => OnItemRemoved.Invoke(x));
            list.RemoveAll(predicate);
        }

        /// <summary>
        /// Removes all of the specified Items from the ItemCollection.
        /// </summary>
        /// <param name="items"></param>
        public void RemoveAll(IEnumerable<T> items)
        {
            Array.ForEach(items.ToArray(), x => OnItemRemoved.Invoke(x));
            list.RemoveAll(x => items.Contains(x));
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
                list.Remove(item);
                OnItemRemoved.Invoke(item);
            }
        }

        /// <summary>
        /// Removes all Items from the ItemCollection.
        /// </summary>
        public void Clear()
        {
            list.ForEach(x => OnItemRemoved.Invoke(x));
            list.Clear();
        }

        /// <summary>
        /// Returns true if the ItemCollection contains the Item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return list.Contains(item);
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
                AddRange(other.list);
                other.list.Clear();
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
                Add(other.list[index]);
                other.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the Item from the other ItemCollection and adds it to this one.
        /// If n is greater than 1, it will remove the first n items from the other ItemCollection.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="item"></param>
        /// <param name="n"></param>
        /// <returns>true if the Items could fit in the Collection</returns>
        public bool TransferItemFromOther(ItemCollection<T> other, T item, int n = 1)
        {
            if(Count + n < Max || Max == -1)
            {
                var others = other.list.FindAll(x => x.Equals(item));
                for(int i = 0; i < n; i++)
                {
                    if(i < others.Count && Count < Max || Max == -1)
                    {
                        var first = other.list.First(x => x.Equals(item));
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
        /// </summary>
        /// <param name="other"></param>
        /// <param name="items"></param>
        /// <returns>true if the Items could fit in the Collection</returns>
        public bool TransferItemsFromOther(ItemCollection<T> other, IEnumerable<T> items)
        {
            if(Count + items.Count() < Max || Max == -1)
            {
                AddRange(other.list.FindAll(x => items.Contains(x)));
                other.RemoveAll(x => items.Contains(x));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the Items from the other ItemCollection and adds them to this one.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="predicate"></param>
        /// <returns>true if the Items could fit in the Collection</returns>
        public bool TransferItemsFromOther(ItemCollection<T> other, Predicate<T> predicate)
        {
            var items = other.list.FindAll(predicate);
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

        /// <summary>
        /// Returns the last item in the list.
        /// </summary>
        /// <returns></returns>
        public T TopItem()
        {
            return list[list.Count - 1];
        }

        /// <summary>
        /// Returns the first item in the list.
        /// </summary>
        /// <returns></returns>
        public T BottomItem()
        {
            return list[0];
        }

        /// <summary>
        /// Returns this ItemCollection as an array.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return list.ToArray();
        }

        /// <summary>
        /// Returns this ItemCollection as a list.
        /// </summary>
        /// <returns></returns>
        public List<T> ToList()
        {
            return list;
        }
    }
}
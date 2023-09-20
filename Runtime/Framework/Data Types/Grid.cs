using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Grid<T> : ItemCollection<GridNode<T>>
{
    public GridNode<T> this[int x, int y]
    {
        get => this[x + y * width];
        set => this[x + y * width] = value;
    }
    public UnityEvent<T> OnItemMoved = new();

    public int width { get => _width; private set => _width = value; }
    public int height { get => _height; private set => _height = value; }

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public T GetValueAt(int x, int y)
    {
        Max = width * height;
        return this[x + y * width].value;
    }

    public void SetValueAt(int x, int y, T value)
    {
        Max = width * height;
        Replace(x + y * width, new GridNode<T>(value, x, y));

        var node = this[x, y];
        node.x = x;
        node.y = y;
    }

    /// <summary>
    /// Moves the Value at the first given Position to the New Position, Invoking the OnItemMoved Event
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
    public void MoveValueAtPositionToPosition(int x, int y, int newX, int newY)
    {
        Max = width * height;
        SetValueAt(newX, newY, GetValueAt(x, y));
        SetValueAt(x, y, default);

        var node = this[x, y];
        node.x = newX;
        node.y = newY;

        OnItemMoved.Invoke(GetValueAt(newX, newY));
    }

    /// <summary>
    /// Moves the given Value to the New Position, Invoking the OnItemMoved Event
    /// </summary>
    /// <param name="value"></param>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
    public void MoveValueToPosition(T value, int newX, int newY)
    {
        Max = width * height;
        var (x, y) = GetPositionOf(value);

        MoveValueAtPositionToPosition(x, y, newX, newY);

        var node = this[x, y];
        node.x = newX;
        node.y = newY;

        OnItemMoved.Invoke(value);
    }

    public (int x, int y) GetPositionOf(T value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (this[i].value.Equals(value))
            {
                return (i % width, i / width);
            }
        }
        return (-1, -1);
    }

    public GridNode<T>[] GetRow(int y)
    {
        List<GridNode<T>> nodes = new();

        for (int i = 0; i < width; i++)
        {
            nodes.Add(this[i, y]);
        }

        return nodes.ToArray();
    }

    public GridNode<T>[] GetRowMatchingPredicate(int y, Predicate<T> predicate)
    {
        List<GridNode<T>> nodes = new();

        for (int i = 0; i < width; i++)
        {
            if (predicate(this[i, y].value))
                nodes.Add(this[i, y]);
        }

        return nodes.ToArray();
    }

    public GridNode<T>[] GetColumn(int x)
    {
        List<GridNode<T>> nodes = new();

        for (int i = 0; i < height; i++)
        {
            nodes.Add(this[x, i]);
        }

        return nodes.ToArray();
    }

    public GridNode<T>[] GetAdjacentNodes(GridNode<T> node)
    {
        List<GridNode<T>> nodes = new();

        if (node.x > 0)
            nodes.Add(this[node.x - 1, node.y]);
        if (node.x < width - 1)
            nodes.Add(this[node.x + 1, node.y]);
        if (node.y > 0)
            nodes.Add(this[node.x, node.y - 1]);
        if (node.y < height - 1)
            nodes.Add(this[node.x, node.y + 1]);

        return nodes.ToArray();
    }

    public GridNode<T>[] GetColumnMatchingPredicate(int x, Predicate<T> predicate)
    {
        List<GridNode<T>> nodes = new();

        for (int i = 0; i < height; i++)
        {
            if (predicate(this[x, i].value))
                nodes.Add(this[x, i]);
        }

        return nodes.ToArray();
    }

    public GridNode<T>[] GetRange(int x, int y, int range)
    {
        List<GridNode<T>> nodes = new();

        for (int i = x - range; i < x + range; i++)
        {
            for (int j = y - range; j < y + range; j++)
            {
                if(i >= 0 && i < width && j >= 0 && j < height)
                    nodes.Add(this[i, j]);
            }
        }

        return nodes.ToArray();
    }

    public GridNode<T>[] GetRangeMatchingPredicate(int x, int y, int range, Predicate<T> predicate)
    {
        List<GridNode<T>> nodes = new();

        for (int i = x - range; i < x + range; i++)
        {
            for (int j = y - range; j < y + range; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height && predicate(this[i, j].value))
                    nodes.Add(this[i, j]);
            }
        }

        return nodes.ToArray();
    }

    public GridNode<T>[] SearchFromPointMatchingPredicate(int x, int y, Predicate<T> predicate)
    {
        var nodesToAdd = GetRangeMatchingPredicate(x, y, 1, predicate).ToList();
        List<GridNode<T>> nodes = new();

        while(nodesToAdd.Count > 0)
        {
            foreach(var node in nodesToAdd.ToArray())
            {
                nodesToAdd.Remove(node);
                if (!nodes.Contains(node))
                {
                    nodes.Add(node);
                    nodesToAdd.AddRange(GetAdjacentNodes(node));
                }
            }
        }

        return nodes.ToArray();
    }
}

[Serializable]
public class GridNode<T>
{
    public T value;
    public int x;
    public int y;
    public GridNode(T value, int x, int y)
    {
        this.value = value;
    }
}
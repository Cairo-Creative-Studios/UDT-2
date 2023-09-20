public interface IGrid<T>
{
    public Grid<T> Grid { get; }

    public T GetValueAt(int x, int y)
    {
        return Grid.GetValueAt(x, y);
    }

    public void SetValueAt(int x, int y, T value)
    {
        Grid.SetValueAt(x, y, value);
    }

    public void MoveValue(int x, int y, int newX, int newY)
    {
        Grid.MoveValueAtPositionToPosition(x, y, newX, newY);
    }

    public void MoveValue(T value, int newX, int newY)
    {
        Grid.MoveValueToPosition(value, newX, newY);
    }

    public (int x, int y) GetPositionOf(T value)
    {
        return Grid.GetPositionOf(value);
    }
}
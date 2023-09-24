using UnityEngine;

public static class NumberExtensions
{
    public static int ConvertRange(this int value, int oldMax, int newMax)
    {
        return (value * newMax) / oldMax;
    }

    public static int ConvertToAngularDirection(this Vector2 vector)
    {
        var angle = Mathf.Atan2(vector.y, -vector.x) * Mathf.Rad2Deg;
        return Mathf.RoundToInt(angle);
    }
}
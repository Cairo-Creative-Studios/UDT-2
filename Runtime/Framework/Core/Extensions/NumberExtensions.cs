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

    public static Vector3 Abs(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

    public static Vector3 Mul(this Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vector2 xy(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static Vector2 xz(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public static Vector2 yz(this Vector3 vector)
    {
        return new Vector2(vector.y, vector.z);
    }

    public static Vector2 LerpAngle(this Vector2 a, Vector2 b, float t)
    {
        return new Vector2(Mathf.LerpAngle(a.x, b.x, t), Mathf.LerpAngle(a.y, b.y, t));
    }

    public static Vector3 LerpAngle(this Vector3 a, Vector3 b, float t)
    {
        return new Vector3(Mathf.LerpAngle(a.x, b.x, t), Mathf.LerpAngle(a.y, b.y, t), Mathf.LerpAngle(a.z, b.z, t));
    }
}
using UnityEngine;

public static class NumberExtensions
{
    private static Vector2 referenceVec2;
    private static Vector3 referenceVec3;

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
        referenceVec3.x = Mathf.Abs(vector.x);
        referenceVec3.y = Mathf.Abs(vector.y);
        referenceVec3.z = Mathf.Abs(vector.z);
        return referenceVec3;
    }

    public static Vector3 Mul(this Vector3 a, Vector3 b)
    {
        referenceVec3.x = a.x * b.x;
        referenceVec3.y = a.y * b.y;
        referenceVec3.z = a.z * b.z;
        return referenceVec3;
    }

    public static Vector2 xy(this Vector3 vector)
    {
        referenceVec2.x = vector.x;
        referenceVec2.y = vector.y;
        return referenceVec2;
    }

    public static Vector2 xz(this Vector3 vector)
    {
        referenceVec2.x = vector.x;
        referenceVec2.y = vector.z;
        return referenceVec2;
    }

    public static Vector2 yz(this Vector3 vector)
    {
        referenceVec2.x = vector.y;
        referenceVec2.y = vector.z;
        return referenceVec2;
    }

    public static Vector2 LerpAngle(this Vector2 a, Vector2 b, float t)
    {
        referenceVec2.x = Mathf.LerpAngle(a.x, b.x, t);
        referenceVec2.y = Mathf.LerpAngle(a.y, b.y, t);
        return referenceVec2;
    }
    public static Vector2 Lerp(this Vector2 a, Vector2 b, float t)
    {
        referenceVec2.x = Mathf.Lerp(a.x, b.x, t);
        referenceVec2.y = Mathf.Lerp(a.y, b.y, t);
        return referenceVec2;
    }

    public static Vector3 LerpAngle(this Vector3 a, Vector3 b, float t)
    {
        referenceVec3.x = Mathf.LerpAngle(a.x, b.x, t);
        referenceVec3.y = Mathf.LerpAngle(a.y, b.y, t);
        referenceVec3.z = Mathf.LerpAngle(a.z, b.z, t);
        return referenceVec3;
    }

    public static Vector3 Lerp(this Vector3 a, Vector3 b, float t)
    {
        referenceVec3.x = Mathf.Lerp(a.x, b.x, t);
        referenceVec3.y = Mathf.Lerp(a.y, b.y, t);
        referenceVec3.z = Mathf.Lerp(a.z, b.z, t);
        return referenceVec3;
    }

    public static Vector3 Lerp(this Vector3 a, Vector3 b, Vector3 t)
    {
        referenceVec3.x = Mathf.Lerp(a.x, b.x, t.x);
        referenceVec3.y = Mathf.Lerp(a.y, b.y, t.y);
        referenceVec3.z = Mathf.Lerp(a.z, b.z, t.z);
        return referenceVec3;
    }
}
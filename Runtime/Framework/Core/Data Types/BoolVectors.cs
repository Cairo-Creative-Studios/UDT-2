using UnityEngine;

public struct Vector2Bool
{
    public bool x;
    public bool y;
    public Vector2 value
    {
        get
        {
            return new Vector2(x ? 1 : 0, y ? 1 : 0);
        }
        set
        {
            x = value.x > 0;
            y = value.y > 0;
        }
    }

    public static Vector2Bool False
    {
        get
        {
            return new Vector2Bool();
        }
    }

    public static Vector2Bool True
    {
        get
        {
            return new Vector2Bool()
            {
                x = true,
                y = true
            };
        }
    }
}

public struct Vector3Bool
{
    public bool x;
    public bool y;
    public bool z;
    public Vector3 value
    {
        get
        {
            return new Vector3(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0);
        }
        set
        {
            x = value.x > 0;
            y = value.y > 0;
            z = value.z > 0;
        }
    }

    public static Vector3Bool False
    {
        get
        {
            return new Vector3Bool();
        }
    }

    public static Vector3Bool True
    {
        get
        {
            return new Vector3Bool()
            {
                x = true,
                y = true,
                z = true
            };
        }
    }
}
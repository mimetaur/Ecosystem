using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 AsVector2(this Vector3 v) {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 AsVector3(this Vector2 v) {
        return new Vector3(v.x, v.y, 0);
    }

    public static float Map(this float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static Vector2 GetRandomPoint(this Bounds b)
    {
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        return new Vector2(x, y);
    }
}

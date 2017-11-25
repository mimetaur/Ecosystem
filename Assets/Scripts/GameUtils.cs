using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameUtils
{

    public static Vector3 SnapTo(Vector3 v3, float snapAngle)
    {
        float angle = Vector3.Angle(v3, Vector3.up);
        if (angle < snapAngle / 2.0f)          // Cannot do cross product 
            return Vector3.up * v3.magnitude;  //   with angles 0 & 180
        if (angle > 180.0f - snapAngle / 2.0f)
            return Vector3.down * v3.magnitude;

        float t = Mathf.Round(angle / snapAngle);
        float deltaAngle = (t * snapAngle) - angle;

        Vector3 axis = Vector3.Cross(Vector3.up, v3);
        Quaternion q = Quaternion.AngleAxis(deltaAngle, axis);
        return q * v3;
    }

    public static Vector2 SnapTo(Vector2 v, float snapAngle)
    {
        var v3 = new Vector3(v.x, v.y, 0);
        return SnapTo(v3, snapAngle);
    }

    public static float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public static Vector2 RandomPointWithinBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}



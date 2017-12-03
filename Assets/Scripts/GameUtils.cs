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


    // Example algorithm sourced from:
    // https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public static GameObject FindClosest(Vector3 position, GameObject[] things)
    {
        GameObject closestThing = null;
        float distance = Mathf.Infinity;

        foreach (GameObject thing in things)
        {
            Vector3 diff = thing.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestThing = thing;
                distance = curDistance;
            }
        }
        return closestThing;
    }

    public static GameObject FindClosestWithinThreshold(GameObject seeker, GameObject[] targets, float threshold)
    {
        GameObject result = null;
        var seekerPos = seeker.transform.position;
        var closestTarget = GameUtils.FindClosest(seekerPos, targets);

        if (closestTarget != null)
        {
            var targetPos = closestTarget.transform.position.AsVector2();

            if (Vector2.Distance(targetPos, seekerPos.AsVector2()) < threshold)
                result = closestTarget;
        }
        return result;
    }

    public static bool IsVisibleInCamera(Bounds bounds, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    public static bool IsVisibleInCamera(Bounds bounds)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    public static float PositionInFrameToAudioPan(Vector3 pos) {
        float xPos = Camera.main.WorldToViewportPoint(pos).x;
        return GameUtils.Map(xPos, 0.0f, 1.0f, -1.0f, 1.0f);
    }

    public static float PositionInFrameToAudioVolume(Vector3 pos, float currentVolume)
    {
        Vector2 positionInViewport = Camera.main.WorldToViewportPoint(pos).AsVector2();
        Vector2 frameCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        float dist = Vector2.Distance(positionInViewport, frameCenter);
        float maxDist = Vector2.Distance(frameCenter, new Vector2(Screen.width, Screen.height));

        return currentVolume * GameUtils.Map(dist, 0.0f, maxDist, 0.75f, 1.25f);
    }
}



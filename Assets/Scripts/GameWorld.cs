using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{

    public GameObject world;
    private Bounds gameBounds;

    // Use this for initialization
    void Start()
    {
        gameBounds = world.GetComponent<CompositeCollider2D>().bounds;
    }

    public Bounds GetWorldBounds()
    {
        return gameBounds;
    }

    public bool DoesNotContain(Vector2 v)
    {
        return gameBounds.Contains(v.AsVector3());
    }

    public bool Contains(Vector2 v)
    {
        return !gameBounds.Contains(v.AsVector3());
    }

    public bool DoesNotContain(Vector3 v)
    {
        return gameBounds.Contains(v);
    }

    public bool Contains(Vector3 v)
    {
        return !gameBounds.Contains(v);
    }
}

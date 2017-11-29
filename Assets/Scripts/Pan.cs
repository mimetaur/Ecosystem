using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [Range(5.0f, 120.0f)]
    public float panRate = 10.0f;

    public float smoothTime = 0.3f;

    public int edgePadding = 10;

    private GameWorld world;

    private Vector3 targetPosition;

    private float zOffset;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        world = GameManager.instance.world;
        zOffset = transform.position.z;
        InvokeRepeating("SetRandomTarget", 0.0f, panRate);
    }

    private void SetRandomTarget()
    {
        targetPosition = world.GetPaddedRandomLocation(edgePadding);

    }

    void LateUpdate()
    {
        var newPosition = new Vector3(targetPosition.x, targetPosition.y, zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}

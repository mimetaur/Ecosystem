using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [Range(5.0f, 120.0f)]
    public float panRate = 10.0f;

    public float smoothTime = 0.3f;

    public int edgePadding = 10;

    public float distanceOffscreen = -200.0f;

    private GameObject targetObject;

    private GameWorld world;

    private Vector3 targetPosition;

    private float zOffset;

    private Vector3 velocity = Vector3.zero;

    //private Bounds targetBounds;

    void Start()
    {
        //targetBounds = targetObject.GetComponent<Collider2D>().bounds;

        world = GameManager.instance.world;
        zOffset = transform.position.z;

        // start "offstage"
        transform.position = new Vector3(distanceOffscreen, 0.0f, zOffset);

        targetObject = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("SetTarget", 1.0f, panRate);
    }

    private void SetTarget()
    {
        // if there's a target, follow it
        // if the target is gone, revert to random panning

        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }
        else
        {
            targetObject = GameObject.FindGameObjectWithTag("Player");
            targetPosition = targetObject.transform.position;
            Invoke("SetTarget", 5.0f);
        }


    }

    void LateUpdate()
    {
        var newPosition = new Vector3(targetPosition.x, targetPosition.y, zOffset);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
}

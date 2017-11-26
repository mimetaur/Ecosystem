using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Based in part on this: https://answers.unity.com/questions/633207/vector2-lerp-probably-a-simple-solution-c.html

public class SmoothCamera2D : MonoBehaviour
{
    public Transform target;

    public float cameraSpeed = 5.0f;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
    }


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, cameraSpeed * Time.deltaTime) + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameBounds : MonoBehaviour
{
    CompositeCollider2D c;
    // Use this for initialization
    void Start()
    {
        c = GetComponent<CompositeCollider2D>();
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        print("Exited");
    }
}

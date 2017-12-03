using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDie : MonoBehaviour
{
    public GameObject icon;

    public void Die() {
        Instantiate(icon, transform.position, Quaternion.identity);
    }
}

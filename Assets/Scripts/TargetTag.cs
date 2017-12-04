using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTag : MonoBehaviour
{

    public string[] tagNames;

    private List<GameObject> targets = new List<GameObject>();

    private void Update()
    {
        foreach (var tagName in tagNames)
        {
            targets.AddRange(GameObject.FindGameObjectsWithTag(tagName));
        }
    }

    public GameObject[] AllTargets()
    {
        return targets.ToArray();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFood : MonoBehaviour
{
    
    public Vector2 Search()
    {
        var foods = GameObject.FindGameObjectsWithTag("Food");
        var food = GetClosestFood(foods);
        if (food != null) {
            return food.transform.position;
        } else {
            return new Vector2(8, -16);
        }

    }

    // Example algorithm sourced from:
    // https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    private GameObject GetClosestFood(GameObject[] foods)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject food in foods)
        {
            Vector3 diff = food.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = food;
                distance = curDistance;
            }
        }
        return closest;
    }
}



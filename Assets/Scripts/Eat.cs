using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetTag))]
public class Eat : MonoBehaviour
{
    private string[] tagNames;
    private AIStateMachine machine;
    private EatSounds eatSounds;
    private WillStarve willStarve;


    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
        tagNames = GetComponent<TargetTag>().tagNames;
        for (int i = 0; i < tagNames.Length; i++) {
            print(tagNames[i]);
        }
        eatSounds = GetComponent<EatSounds>();
        willStarve = GetComponent<WillStarve>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckEating(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CheckEating(collider.gameObject);
    }

    private void CheckEating(GameObject other)
    {
        for (int i = 0; i < tagNames.Length; i++)
        {
            if (other.tag == tagNames[i])
            {
                EatOther(other);
            }

        }
    }

    private void EatOther(GameObject other)
    {
        //string debug = string.Format("{0} ate {1} at {2}", name, other.name, other.transform.position.AsVector2());
        //print(debug);

        if (eatSounds != null) eatSounds.Play();
        if (willStarve != null) willStarve.DidEat();

        var onDie = other.GetComponent<OnDie>();
        if (onDie != null) onDie.Die();

        machine.currentState = AIStateMachine.State.Wander;
        Destroy(other.gameObject);
    }
}

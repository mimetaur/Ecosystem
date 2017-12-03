using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetTag))]
public class Eat : MonoBehaviour
{
    private string tagName;
    private AIStateMachine machine;
    private EatSounds eatSounds;
    private WillStarve willStarve;

    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
        var targetTag = GetComponent<TargetTag>();
        tagName = targetTag.tagName;
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

    private void CheckEating(GameObject other) {
        if (other.tag == tagName)
            EatOther(other);
    }

    private void EatOther(GameObject other) {
        string debug = string.Format("{0} ate {1} at {2}", name, other.name, other.transform.position.AsVector2());
        print(debug);

        if (eatSounds != null) eatSounds.Play();
        if (willStarve != null) willStarve.DidEat();

        machine.currentState = AIStateMachine.State.Wander;
        Destroy(other.gameObject);
    }
}

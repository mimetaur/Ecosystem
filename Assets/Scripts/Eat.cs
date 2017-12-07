using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TargetTag))]
public class Eat : MonoBehaviour
{
    private string tagName;
    private AIStateMachine machine;
    private EatSounds eatSounds;
    private WillStarve willStarve;
    public float dieTime = 0.5f;


    private void Start()
    {
        machine = GetComponent<AIStateMachine>();
        tagName = GetComponent<TargetTag>().tagName;
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
        if (other.tag == tagName || (this.gameObject.tag == "Enemy" && other.tag == "Player"))
        {
            EatOther(other);
        }

    }

    private void EatOther(GameObject other)
    {
        if (other == null) return;

        if (eatSounds != null) eatSounds.Play();
        if (willStarve != null) willStarve.DidEat();

        var onDie = other.GetComponent<OnDie>();
        if (onDie != null) onDie.Die();

        machine.currentState = AIStateMachine.State.Wander;

        if (this.gameObject.tag == "Enemy" && other.tag == "Player")
        {
            other.SetActive(false);
            GameManager.instance.RespawnPlayer();
        }
        else
        {
            if (onDie == null)
            {
                if (other.tag == "Mushroom" || other.tag == "Coin")
                {
                    other.transform.DOScale(0, dieTime).OnComplete(() => DestroyCallback(other));
                }
                else
                {
                    Destroy(other);
                }

            }
        }

    }

    private void DestroyCallback(GameObject other)
    {
        Destroy(other);
    }
}

﻿using System.Collections;
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
            Destroy(other.gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Steer : MonoBehaviour
{

    public float maxSeeAhead = 10.0f;

    public GameObject destination;
    private Vector2 target;

    private Rigidbody2D rb2d;

    private float maxSpeed;
    private float maxAvoidForce;

    private SpriteRenderer sr;
    public AudioClip clip;
    public Range volumeRange;

    [System.Serializable]
    public class Range
    {
        public float min = 0.25f;
        public float max = 0.75f;
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        destination = GameObject.Find("Roller Die");
        var bounds = destination.GetComponent<BoxCollider2D>().bounds;
        target = bounds.GetRandomPoint();

        maxSpeed = Random.Range(2.5f, 5.0f);
        maxAvoidForce = 4.0f;
    }



    private Vector2 Seek(Vector2 targetPosition)
    {
        var position = transform.position.AsVector2();


        Vector2 desired = targetPosition - position;
        desired.Normalize();
        desired *= maxSpeed;

        Vector2 steer = desired - rb2d.velocity;
        steer.Normalize();

        steer *= maxSpeed;
        return steer;
    }

    private Vector2 Avoid()
    {
        Vector2 position = transform.position.AsVector2();

        //RaycastHit2D
        RaycastHit2D hit = Physics2D.Raycast(position, rb2d.velocity.normalized, maxSeeAhead);

        // avoidance_force = ahead - obstacle_center

        Vector2 ahead = position + rb2d.velocity;

        // by default, no avoidance
        Vector2 force = new Vector2(0, 0);
        if (hit.collider != null)
        {
            print("About to collide" + hit.fraction);
            Vector2 obstacle = hit.collider.transform.position;
            force = ((ahead - obstacle).normalized) * maxAvoidForce;
        }

        return force;
    }

    private void FixedUpdate()
    {
        var seek = Seek(target);
        rb2d.AddForce(seek);

        //var avoid = Avoid();

        //rb2d.AddForce(avoid);

        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

        rb2d.angularVelocity = maxSpeed.Map(2.5f, 5.0f, 100.0f, 150.0f);
        //transform.Rotate( new Vector3(0, 0 1.0f) );
    }

    private void OnCollisionEnter2d(Collision2D coll)
    {

    }

    private void PlayThud()
    {
        if (!GameUtils.IsVisibleInCamera(sr.bounds, Camera.main)) return;

        float vol = Random.Range(volumeRange.min, volumeRange.max);
        vol = GameUtils.PositionInFrameToAudioVolume(transform.position, vol);
        float pan = GameUtils.PositionInFrameToAudioPan(transform.position);

        SoundManager.instance.PlaySound(clip, vol, pan);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSounds : MonoBehaviour
{

    public AudioClip[] sounds;

    public Range volumeRange;

    private SpriteRenderer sr;
    private AudioSource source;


    [System.Serializable]
    public class Range
    {
        public float min = 0.5f;
        public float max = 1.0f;
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void Play()
    {
        if ( !GameUtils.IsVisibleInCamera(sr.bounds, Camera.main) ) return;
        int randomIdx = Random.Range(0, sounds.Length);
        var sound = sounds[randomIdx];

        var volume = Random.Range(volumeRange.min, volumeRange.max);
        source.PlayOneShot(sound, volume);
    }
}

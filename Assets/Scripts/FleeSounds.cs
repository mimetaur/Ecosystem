using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeSounds : MonoBehaviour
{

    public AudioClip[] clips;
    public Range volumeRange;

    private SpriteRenderer sr;


    [System.Serializable]
    public class Range
    {
        public float min = 0.25f;
        public float max = 0.75f;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    public void Play()
    {
        if (!GameUtils.IsVisibleInCamera(sr.bounds, Camera.main)) return;

        float vol = Random.Range(volumeRange.min, volumeRange.max);
        vol = GameUtils.PositionInFrameToAudioVolume(transform.position, vol);
        float pan = GameUtils.PositionInFrameToAudioPan(transform.position);

        SoundManager.instance.PlayRandomizedSound(clips, vol, pan);
    }
}

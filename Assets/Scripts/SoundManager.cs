using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public Range volumeRange;

    private AudioSource source;

    [System.Serializable]
    public class Range
    {
        public float min = 0.5f;
        public float max = 0.75f;
    }

    private void Awake()
    {


        if (instance == null) // check if the instance already exists
        {
            instance = this;
        }
        else if (instance != this) // check if instance already exists and it's not this
        {
            Destroy(gameObject); // then destroy this, there can be only one
        }

        // preserve between scenes
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        float volume = Random.Range(volumeRange.min, volumeRange.max);
        PlaySound(clip, volume);
    }

    public void PlaySound(AudioClip clip, float volume, float pan = 0.0f)
    {
        source.panStereo = pan;
        source.PlayOneShot(clip, volume);
    }

    public void PlayRandomizedSound(AudioClip[] clips)
    {
        float volume = Random.Range(volumeRange.min, volumeRange.max);
        PlayRandomizedSound(clips, volume);
    }

    public void PlayRandomizedSound(AudioClip[] clips, float volume, float pan = 0.0f)
    {
        int randomIdx = Random.Range(0, clips.Length);
        var clip = clips[randomIdx];

        source.panStereo = pan;

        source.PlayOneShot(clip, volume);
    }
}

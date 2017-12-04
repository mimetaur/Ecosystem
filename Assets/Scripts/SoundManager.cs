using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public Range volumeRange;

    public bool varyPitch = true;

    [Range(0.01f, 0.5f)]
    public float pitchWaver;

    public AudioSource fxSource;
    public AudioSource musicSource;

    public AudioClip musicLoop;

    public float musicDelay;

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

    }

    private void Start()
    {
        musicSource.clip = musicLoop;
        musicSource.loop = true;
        musicSource.PlayDelayed(musicDelay);
    }

    public void PlaySound(AudioClip clip)
    {
        float volume = Random.Range(volumeRange.min, volumeRange.max);
        PlaySound(clip, volume);
    }

    public void PlaySound(AudioClip clip, float volume, float pan = 0.0f)
    {
        fxSource.panStereo = pan;
        if (varyPitch) fxSource.pitch = 1.0f + Random.Range(-pitchWaver, pitchWaver);
        fxSource.PlayOneShot(clip, volume);
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

        fxSource.panStereo = pan;
        if (varyPitch) fxSource.pitch = 1.0f + Random.Range(-pitchWaver, pitchWaver);
        fxSource.PlayOneShot(clip, volume);
    }
}

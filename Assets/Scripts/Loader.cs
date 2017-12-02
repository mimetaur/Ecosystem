using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public SoundManager soundManager;

	// Use this for initialization
	void Awake () {
        if (SoundManager.instance == null)
            Instantiate(soundManager);
	}
}

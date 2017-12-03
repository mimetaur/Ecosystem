using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameManager gameManager;
    public SoundManager soundManager;


	// Use this for initialization
	void Awake () {

        if (GameManager.instance == null) {
            Instantiate(gameManager);
        }

        if (SoundManager.instance == null)
            Instantiate(soundManager);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameWorld world;

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

        // keep a reference to the gameWorld script
        world = GetComponent<GameWorld>();

        InitializeGame();
    }

    private void InitializeGame() {
        world.Initialize();
    }
}

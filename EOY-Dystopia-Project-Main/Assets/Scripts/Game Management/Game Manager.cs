using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isPaused = false;
    
    public bool freezePlayerMovement = false;
    public bool freezePlayerLook = false;
    
    public bool canInteract = true;
    
    public InputManager InputManager {get ; private set;}
    public UIManager UIManager { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InputManager = GetComponent<InputManager>();
        UIManager = GetComponent<UIManager>();
    }

    private void Update()
    {
       
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         isPaused = !isPaused;
    //     }
    //
    //     Time.timeScale = isPaused ? 0 : 1;
    // }
}

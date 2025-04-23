using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public bool firstBoot = true;

    public bool isPaused = false;
    
    public bool freezePlayerMovement = false;
    public bool freezePlayerLook = false;
    
    public bool canInteract = true;
    
    public EventManager EventManager { get; private set; }
    public InputManager InputManager {get ; private set;}
    public UIManager UIManager { get; private set; }
    public SceneController SceneController { get; private set; }
    public EnemyController EnemyController { get; private set; }
    public ResourceManagement ResourceManager { get; private set; }
    

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
        EventManager = GetComponent<EventManager>();
        InputManager = GetComponent<InputManager>();
        UIManager = GetComponent<UIManager>();
        SceneController = GetComponent<SceneController>();
        EnemyController = FindObjectOfType<EnemyController>();
        ResourceManager = GetComponent<ResourceManagement>();
        
    }
    
    private void Start()
    {
      firstBoot = true;
      Instance.EventManager.GameBoot();
        
        // InputManager.InputMap.UI.Pause.performed += ctx => UIManager.PauseMenu();
        // InputManager.InputMap.UI.Resume.performed += ctx => UIManager.ResumeGame();
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

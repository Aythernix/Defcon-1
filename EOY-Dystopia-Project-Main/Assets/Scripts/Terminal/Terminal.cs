
using System;
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    
    public string InteractText => "Access Terminal";
    public bool Hold { get; set; } = false;
    public bool Interactable { get; private set; } = true;

    [SerializeField]private BunkerData _bunkerData;
    [SerializeField]private TextMeshProUGUI _bunkerHealthText;
    
    public GameObject terminalCamera;
    public GameObject playerCamera;
    public Animator TerminalAnimator;
    public Canvas terminalCanvas;
    

    public bool Interact(Interactor interactor)
    {
        
        GameManager.Instance.AudioManager.PlayTerminalSounds(GameManager.Instance.AudioManager.TerminalEnter);
        
        playerCamera.gameObject.SetActive(false);
        terminalCamera.SetActive(true);
        
        TerminalAnimator.Play("Terminal Camera");
        
        LockPlayer();
        
        GameManager.Instance.InputManager.InputMap.Terminal.Enable();
        return true;
    }
    
    private void LockPlayer()
    {
        GameManager.Instance.isInTerminal = true;
        GameManager.Instance.freezePlayerLook = true;
        GameManager.Instance.freezePlayerMovement = true;
        GameManager.Instance.canInteract = false;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void UnlockPlayer()
    {
        GameManager.Instance.isInTerminal = false;
        GameManager.Instance.freezePlayerLook = false;
        GameManager.Instance.freezePlayerMovement = false;
        GameManager.Instance.canInteract = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TurretCamButton()
    {
        StartCoroutine(GameManager.Instance.SceneController.OutsideScene());
    }

    private void ExitTerminal()
    {
        GameManager.Instance.InputManager.InputMap.Terminal.Disable();
        
        terminalCamera.SetActive(false);
        playerCamera.SetActive(true);
        
       UnlockPlayer();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        terminalCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.InputManager.InputMap.Terminal.Exit.triggered)
        {
            ExitTerminal();
        }
        _bunkerHealthText.text = $"Bunker Health: {Mathf.RoundToInt(_bunkerData.BunkerHealth)}/{_bunkerData.BunkerMaxHealth}";
        
        terminalCanvas.enabled = GameManager.Instance.PowerSystem.isPowerActive;
        Interactable = GameManager.Instance.PowerSystem.isPowerActive;
        
        
    }
    
}


   
   

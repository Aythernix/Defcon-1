
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    
    public string InteractText => "Access Terminal";
    public bool Hold { get; set; } = false;
    public bool Interactable { get; } = true;

    [SerializeField]private BunkerData _bunkerData;
    [SerializeField]private TextMeshProUGUI _bunkerHealthText;
    
    public GameObject terminalCamera;
    public GameObject playerCamera;
    public Animator TerminalAnimator;
    

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with Terminal");
        
        GameManager.Instance.freezePlayerLook = true;
        GameManager.Instance.freezePlayerMovement = true;
        GameManager.Instance.canInteract = false;
        
        playerCamera.gameObject.SetActive(false);
        terminalCamera.SetActive(true);
        
        TerminalAnimator.Play("Terminal Camera");
        
        GameManager.Instance.InputManager.InputMap.Terminal.Enable();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        return true;
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
        
        GameManager.Instance.freezePlayerLook = false;
        GameManager.Instance.freezePlayerMovement = false;
        GameManager.Instance.canInteract = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        _bunkerHealthText.text = $"Bunker Health: {_bunkerData.BunkerHealth}/{_bunkerData.BunkerMaxHealth}";
    }
    
}

   
   

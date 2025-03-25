
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    
    public string InteractText => "Access Terminal";

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
        
        
        return true;
    }

    private void ExitTerminal()
    {
        GameManager.Instance.InputManager.InputMap.Terminal.Disable();
        
        terminalCamera.SetActive(false);
        playerCamera.SetActive(true);
        
        GameManager.Instance.freezePlayerLook = false;
        GameManager.Instance.freezePlayerMovement = false;
        GameManager.Instance.canInteract = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        terminalCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.InputManager.InputMap.Terminal.Exit.performed += ctx => ExitTerminal();
    }
    
}

   
   

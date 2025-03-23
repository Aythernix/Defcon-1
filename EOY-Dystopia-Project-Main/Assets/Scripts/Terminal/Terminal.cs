
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    
    public string InteractText => "Interact with Terminal";

    public GameObject terminalCamera;
    public Animator TerminalAnimator;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with Terminal");
        
        GameManager.Instance.freezePlayerLook = true;
        GameManager.Instance.freezePlayerMovement = true;
        GameManager.Instance.canInteract = false;
        
        Camera.main.gameObject.SetActive(false);
        terminalCamera.SetActive(true);
        
        TerminalAnimator.Play("Terminal Camera");
        
        return true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        terminalCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

   
   

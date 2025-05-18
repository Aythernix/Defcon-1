using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    public string InteractText { get; }
    public bool Interact(Interactor interactor);
    
    public bool Interactable { get; }
    public bool Hold { get; set; }
   
    
}
public class Interactor: MonoBehaviour
{
    
    private InputManager _Controls;

    public Transform InteractorSource;
    public float InteractionDistance = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        _Controls = GameManager.Instance.InputManager;
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        
    }

    private void Interact()
    {
        if (!GameManager.Instance.canInteract)
        {
            GameManager.Instance.UIManager.HideInteractionPrompt();
            return;
        }
        
        // Raycast from the InteractorSource
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        
        // If the ray hits an object that implements IInteractable
        if (Physics.Raycast(ray, out RaycastHit hit, InteractionDistance))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable InteractObj) && InteractObj.Interactable)
            {
                if (!hit.collider.gameObject.GetComponent<IInteractable>().Hold)
                {
                    if (_Controls.InputMap.Player.Interact.WasPressedThisFrame())
                    {
                        InteractObj.Interact(this);
                    }
                }
                else
                {
                    if (_Controls.InputMap.Player.Interact.ReadValue<float>() > 0)
                    {
                        InteractObj.Interact(this);
                    }
                }
                
                GameManager.Instance.UIManager.ShowInteractionPrompt(InteractObj.InteractText);
                
                if (InteractObj.Hold)
                {
                    GameManager.Instance.UIManager.ShowInteractionHold();
                }
            }
            else
            {
                GameManager.Instance.UIManager.HideInteractionPrompt();
                GameManager.Instance.UIManager.HideInteractionHold();
                
            }
        }
        else
        {
            GameManager.Instance.UIManager.HideInteractionPrompt();
        }
        
        Debug.DrawRay(ray.origin, ray.direction * InteractionDistance, Color.red);
    }
}

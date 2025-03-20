using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractable
{
    public void Interact();
}
public class InteractionController : MonoBehaviour
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
        if (_Controls.InputMap.Player.Interact.triggered)
        {
            Interact();
        }
    }

    private void Interact()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hit, InteractionDistance))
        {
            if (hit.collider.gameObject.TryGetComponent(out IInteractable InteractObj))
            {
                InteractObj.Interact();
            }
        }
    }
}

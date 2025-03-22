using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    
    public string InteractText => "Interact with Terminal";

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with Terminal");
        return true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}

   
   

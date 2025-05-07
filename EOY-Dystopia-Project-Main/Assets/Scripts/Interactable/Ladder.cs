using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    public string InteractText => "Climb Ladder";
    public bool Hold { get; } = false;
    
    [Header("Ladder Config")]
    [SerializeField] private Transform _ladderTop;
    [SerializeField] private Transform _ladderBottom;

    public bool Interact(Interactor interactor)
    {
        if (Vector3.Distance(interactor.transform.position, _ladderTop.position) > Vector3.Distance(interactor.transform.position, _ladderBottom.position))
        {
            interactor.GetComponent<CharacterController>().enabled = false;
            interactor.gameObject.transform.position = _ladderTop.position;
            interactor.GetComponent<CharacterController>().enabled = true;
            Debug.Log("went top");
        }
        else
        {
            interactor.GetComponent<CharacterController>().enabled = false;
            interactor.gameObject.transform.position = _ladderBottom.position;
            interactor.GetComponent<CharacterController>().enabled = true;
            Debug.Log("went bottom");   
        }
        
        return true;
    }
}
    

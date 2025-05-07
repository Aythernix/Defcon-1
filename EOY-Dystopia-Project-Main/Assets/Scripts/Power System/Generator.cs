using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    [Header("Generator Config")]
    [SerializeField] private float _interactHoldTime = 5f;

    private float _interactTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string InteractText { get; } = "Repair Generator";
    public bool Hold { get; } = true;

    public bool Interact(Interactor interactor)
    {
        _interactTime += Time.deltaTime;

        if (_interactTime >= _interactHoldTime)
        {
            InteractionComplete();
        }

        return true;
    }

    private void InteractionComplete()
    {
        _interactTime = 0;
        Debug.Log("Generator Completed");
    }



}

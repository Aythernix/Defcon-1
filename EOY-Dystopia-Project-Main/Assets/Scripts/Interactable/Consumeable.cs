using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumeable : MonoBehaviour, IInteractable
{
    
    [Header("Consumable Type")]
    [SerializeField] private ResourceManagement.ConsumableType _ConsumableType;
    
    [Header("Consumable Config")]
    [SerializeField] private float _ConsumeableAmount;
    
    public string InteractText => $"Consume {_ConsumableType}";
    
    public bool Interact(Interactor interactor)
    {
        GameManager.Instance.ResourceManager.Consume(_ConsumableType, _ConsumeableAmount);
        
        return true;
    }
}



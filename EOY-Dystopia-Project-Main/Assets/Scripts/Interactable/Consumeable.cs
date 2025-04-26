using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumeable : MonoBehaviour, IInteractable
{
    
    [Header("Consumable Type")]
    [SerializeField] private ResourceManagement.ConsumableType _ConsumableType;
    
    [Header("Consumable Config")]
    [SerializeField] private int _maxConsumableAmount = 50;
    private int _currentConsumableAmount;
    [SerializeField] private float _ConsumeAmount;
    
    public string InteractText => $"Consume {_ConsumableType}";
    
    public bool Interact(Interactor interactor)
    {
        GameManager.Instance.ResourceManager.Consume(_ConsumableType, _ConsumeAmount);
        _currentConsumableAmount--;
        
        return true;
    }
    
    public void RestockConsumable()
    {
        _currentConsumableAmount = _maxConsumableAmount;
    }
    
}



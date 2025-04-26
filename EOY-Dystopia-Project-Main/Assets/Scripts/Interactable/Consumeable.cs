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
    [SerializeField] private float _ConsumeAmount = 5f;
    
    public string InteractText => $"Consume {_ConsumableType}, {_currentConsumableAmount} left";
    
    private void Start()
    {
        _currentConsumableAmount = _maxConsumableAmount;
    }
    
    public bool Interact(Interactor interactor)
    {
        if (_currentConsumableAmount > 0)
        {
            GameManager.Instance.ResourceManager.Consume(_ConsumableType, _ConsumeAmount);
            _currentConsumableAmount--;
            
            Debug.Log($"Consumed {_ConsumableType}, Remaining: {_currentConsumableAmount}");
        }
        return true;
    }
    
    public void RestockConsumable()
    {
        _currentConsumableAmount = _maxConsumableAmount;
    }
    
}



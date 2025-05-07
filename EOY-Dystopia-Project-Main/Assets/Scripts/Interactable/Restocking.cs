using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restocking : MonoBehaviour, IInteractable
{
    [Header("Consumable Type")]
    [SerializeField] private ResourceManagement.ConsumableType _ConsumableType;
    
    public string InteractText => $"Restock {_ConsumableType}";
    public bool Hold { get; } = false;
    
    public bool Interact(Interactor interactor)
    {

        Consumeable[] consumables = FindObjectsOfType<Consumeable>();
        foreach (Consumeable consumable in consumables)
        {
            if (consumable.ConsumableType == _ConsumableType)
            {
                consumable.RestockConsumable();
                Debug.Log($"Restocked {consumable.ConsumableType}");
            }
        }

        return true;
    }
}


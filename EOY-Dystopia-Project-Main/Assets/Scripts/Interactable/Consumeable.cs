
using UnityEngine;

public class Consumeable : MonoBehaviour, IInteractable
{
    
    [Header("Consumable Type")] 
    public ResourceManagement.ConsumableType ConsumableType;
    
    [Header("Consumable Config")]
    [SerializeField] private int _maxConsumableAmount = 50;
    private int _currentConsumableAmount;
    [SerializeField] private float _ConsumeAmount = 5f;
    [SerializeField] private float _ConsumableID;
    
    public string InteractText => $"Consume {ConsumableType}, {_currentConsumableAmount} left";
    public bool Hold { get; } = false;

    private void Start()
    {
        _currentConsumableAmount = PlayerPrefs.HasKey($"Consumable {_ConsumableID}") ? PlayerPrefs.GetInt($"Consumable {_ConsumableID}") : _maxConsumableAmount;
        Debug.Log(PlayerPrefs.HasKey($"Consumable {_ConsumableID}"));
    }
    
    public bool Interact(Interactor interactor)
    {
        if (_currentConsumableAmount > 0)
        {
            GameManager.Instance.ResourceManager.Consume(ConsumableType, _ConsumeAmount);
            _currentConsumableAmount--;
        }
        
        PlayerPrefs.SetInt($"Consumable {_ConsumableID}", _currentConsumableAmount);
        return true;
    }
    
    public void RestockConsumable()
    {
        _currentConsumableAmount = _maxConsumableAmount;
        PlayerPrefs.SetInt($"Consumable {_ConsumableID}", _currentConsumableAmount);
    }
        
}



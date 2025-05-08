using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ResourceManagement : MonoBehaviour
{
    public float playerHunger;
    public float playerThirst;
    
    [Header("Resource Config")]
    [SerializeField] private float _maxHunger = 100f;
    [SerializeField] private float _maxThirst = 100f;
    [SerializeField] private float _hungerDecreaseTime = 10f;
    [SerializeField] private float _hungerDecreaseAmount = 1f;
    [SerializeField] private float _thirstDecreaseTime = 10f;
    [SerializeField] private float _thirstDecreaseAmount = 1f;
    public float timeBeforeDeath { private set; get; } = 10f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerHunger = _maxHunger;
        playerThirst = _maxThirst;
        StartCoroutine(ThirstDecreaseOverTime());
        StartCoroutine(HungerDecreaseOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHunger <= 0)
        {
            // Handle player death due to hunger
            GameManager.Instance.EventManager.EmptyResourceState(ConsumableType.Food);
            Debug.Log("Player has died from hunger.");
        }

        if (playerThirst <= 0)
        {
            // Handle player death due to thirst
            GameManager.Instance.EventManager.EmptyResourceState(ConsumableType.Water);
            Debug.Log("Player has died from thirst.");
        }
        
        playerHunger = Mathf.Clamp(playerHunger, 0, _maxHunger);
        playerThirst = Mathf.Clamp(playerThirst, 0, _maxThirst);
        
        HandleUI();
        
    }
    
    // Enum of all consumable types
    public enum ConsumableType
    {
        Food,
        Water,
    }
    
    // Switch statement to determine which resource to consume
    public void Consume(ConsumableType consumableType, float consumeAmount)
    {
        switch (consumableType)
        {
            case ConsumableType.Food:
                playerHunger += consumeAmount;
                break;
            case ConsumableType.Water:
                playerThirst += consumeAmount;
                break;
        }
    }
    
    // Coroutine to decrease thirst over time
    private IEnumerator ThirstDecreaseOverTime()
    {
        yield return new WaitForSeconds(_thirstDecreaseTime);
        playerThirst -= _thirstDecreaseAmount;

        StartCoroutine(ThirstDecreaseOverTime());
    }
    
    // Coroutine to decrease hunger over time
    private IEnumerator HungerDecreaseOverTime()
    {
        yield return new WaitForSeconds(_hungerDecreaseTime);
        playerHunger -= _hungerDecreaseAmount;
        
        StartCoroutine(HungerDecreaseOverTime());
    }

    private void HandleUI()
    {
        // Update the UI with the current hunger and thirst values
        GameManager.Instance.UIManager.HungerBarUpdater(playerHunger / 100);
        GameManager.Instance.UIManager.ThirstBarUpdater(playerThirst / 100);
    }
}

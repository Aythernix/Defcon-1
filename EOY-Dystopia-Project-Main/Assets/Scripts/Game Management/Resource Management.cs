using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class ResourceManagement : MonoBehaviour
{
    public float playerHunger;
    public float playerThirst;
    public float money;
    
    [Header("Resource Config")]
    public float maxHunger = 100f;
    public float maxThirst = 100f;
    [SerializeField] float _currentTimeBeforeDeath;
    [SerializeField] private float _hungerDecreaseTime = 10f;
    [SerializeField] private float _hungerDecreaseAmount = 1f;
    [SerializeField] private float _thirstDecreaseTime = 10f;
    [SerializeField] private float _thirstDecreaseAmount = 1f;
    public float timeBeforeDeath { private set; get; } = 15f;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerHunger = maxHunger;
        playerThirst = maxThirst;
        StartCoroutine(ThirstDecreaseOverTime());
        StartCoroutine(HungerDecreaseOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        if(playerThirst <= 0 || playerHunger <= 0)
        {
            NoThirstOrHunger();
            GameManager.Instance.UIManager.consumableWarning.SetActive(true);
        }
        else
        {
            _currentTimeBeforeDeath = timeBeforeDeath;
            GameManager.Instance.UIManager.consumableWarning.SetActive(false);
        }
        
        playerHunger = Mathf.Clamp(playerHunger, 0, maxHunger);
        playerThirst = Mathf.Clamp(playerThirst, 0, maxThirst);
        
        HandleUI();
        
    }
    
    private void NoThirstOrHunger()
    {
        _currentTimeBeforeDeath -= Time.deltaTime;
        _currentTimeBeforeDeath = Mathf.Clamp(_currentTimeBeforeDeath, 0, timeBeforeDeath);
        GameManager.Instance.UIManager.consumableWarning.GetComponentInChildren<TextMeshProUGUI>().text = $"Consume or Die: {Mathf.RoundToInt(_currentTimeBeforeDeath)}";

        if (_currentTimeBeforeDeath <= 0f)
        {
            GameManager.Instance.EventManager.GameOver(true, "Failed to consume in time");
        }
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
    
    // Method to add money
    public void AddMoney(float amount)
    {
        money += amount;
    }
    // Method to remove money
    public void RemoveMoney(float amount)
    {
        money -= amount;
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
    
    
    // Coroutine to handle the UI updates
    private void HandleUI()
    {
        // Update the UI with the current hunger and thirst values
        GameManager.Instance.UIManager.HungerBarUpdater(playerHunger / 100);
        GameManager.Instance.UIManager.ThirstBarUpdater(playerThirst / 100);
    }
}

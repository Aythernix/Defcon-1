using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagement : MonoBehaviour
{
    public float playerHunger;
    public float PlayerThirst;
    
    [Header("Resource Config")]
    [SerializeField] private float _hungerDecreaseTime = 10f;
    [SerializeField] private float _hungerDecreaseAmount = 1f;
    [SerializeField] private float _thirstDecreaseTime = 10f;
    [SerializeField] private float _thirstDecreaseAmount = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThirstDecreaseOverTime());
        StartCoroutine(HungerDecreaseOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHunger <= 0)
        {
            // Handle player death due to hunger
            Debug.Log("Player has died from hunger.");
        }
        if (PlayerThirst <= 0)
        {
            // Handle player death due to thirst
            Debug.Log("Player has died from thirst.");
        }
    }
    
    public void Drink(float amount)
    {
        PlayerThirst += amount;
        PlayerThirst = Mathf.Clamp(PlayerThirst, 0, 100);
    }
    
    public void Eat(float amount)
    {
        playerHunger += amount;
        playerHunger = Mathf.Clamp(playerHunger, 0, 100);
    }
    
    private IEnumerator ThirstDecreaseOverTime()
    {
        yield return new WaitForSeconds(_thirstDecreaseTime);
        PlayerThirst -= _thirstDecreaseAmount;

        StartCoroutine(ThirstDecreaseOverTime());
    }
    
    private IEnumerator HungerDecreaseOverTime()
    {
        yield return new WaitForSeconds(_hungerDecreaseTime);
        playerHunger -= _hungerDecreaseAmount;
        
        StartCoroutine(HungerDecreaseOverTime());
    }

    private void HandleUI()
    {
        // Update the UI with the current hunger and thirst values
        
        // GameManager.Instance.UIManager.UpdateHungerBar(hunger);
        // GameManager.Instance.UIManager.UpdateThirstBar(thirst);
    }
}

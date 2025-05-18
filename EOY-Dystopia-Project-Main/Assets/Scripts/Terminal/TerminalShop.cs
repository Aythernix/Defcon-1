
using System;
using TMPro;
using UnityEngine;

public class TerminalShop : MonoBehaviour
{
    [Header("Terminal Screens")]
    public GameObject terminal;
    public GameObject shop;
    
    [Header("Shop Config")]
    public GameObject MoneyText;
    public GameObject RepairBunkerText;
    
    [Header("Shop Assets")]
    public ShopAssets repairBunker;

    private void Start()
    {
        CloseShop();
        RepairBunkerText.GetComponent<TextMeshProUGUI>().text = $"<Repair Bunker: {repairBunker.assetCost}>";
    }

    private void Update()
    {
        MoneyText.GetComponent<TextMeshProUGUI>().text = $"Money: {GameManager.Instance.ResourceManager.money}";
    }

    public void OpenShop()
    {
        terminal.SetActive(false);
        shop.SetActive(true);
    }
    public void CloseShop()
    {
        terminal.SetActive(true);
        shop.SetActive(false);
    }
    
    

    #region ShopStock
    public void BuyRepairBunker()
    {
        if (GameManager.Instance.ResourceManager.money >= repairBunker.assetCost)
        {
            GameManager.Instance.ResourceManager.RemoveMoney(repairBunker.assetCost);
            GameManager.Instance.RepairBunker();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    #endregion
}
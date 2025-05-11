using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject interactionText;
    public GameObject holdIndicator;
    public GameObject ammoCountText;
    public GameObject turretCountText;
    public GameObject turretCooldownBar;
    public GameObject hungerBar;
    public GameObject thirstBar;
    public GameObject consumableWarning;
    public GameObject endGameScreen;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (interactionText != null)
        {
            interactionText.transform.parent.gameObject.SetActive(false);
        }
        
        endGameScreen.SetActive(false);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ammoCountText = GameObject.Find("Ammo Count");
        turretCountText = GameObject.Find("Turret Count");
        interactionText = GameObject.Find("Interaction Text");
        holdIndicator = GameObject.Find("Hold Amount");
        turretCooldownBar = GameObject.Find("Turret Cooldown Bar");
    }
    
    #region Interactions UI
    // Show the interaction prompt with the given text
    public void ShowInteractionPrompt(string text)
    {
        interactionText.transform.parent.gameObject.SetActive(true);
        interactionText.GetComponentInChildren<TextMeshProUGUI>().text = text + " : " + "[ " + GameManager.Instance.InputManager.InputMap.Player.Interact.GetBindingDisplayString() + " ]";
    }
    // Hide the interaction prompt
    public void HideInteractionPrompt()
    {
        interactionText.transform.parent.gameObject.SetActive(false);
        interactionText.GetComponentInChildren<TextMeshProUGUI>().text = "No Prompt (This is a bug...)";
    }

    public void ShowInteractionHold()
    {
        holdIndicator.transform.parent.gameObject.SetActive(true);
        
    }
    public void HideInteractionHold()
    {
        holdIndicator.transform.parent.gameObject.SetActive(false);
        holdIndicator.GetComponent<Image>().fillAmount = 0;
    }
    
    public void UpdateHoldIndicator(float fillAmount)
    {
        holdIndicator.GetComponent<Image>().fillAmount = fillAmount;
    }
    #endregion

    #region Turret UI
    
    // Update the ammo count text
    public void AmmoCountUpdater(string text, int? currentAmmo, int? maxAmmo)
    {
        
        if (ammoCountText != null)
        {
            if (text != null)
            {
                ammoCountText.GetComponent<TextMeshProUGUI>().text = text;
            }
            else
            {
                ammoCountText.GetComponent<TextMeshProUGUI>().text = currentAmmo + "/" + maxAmmo;
            }
        }
        else
        {
            Debug.LogWarning("Ammo Count Text is null");
        }
    }
    
    // Update the turret count text
    public void TurretCountUpdater(int currentTurrets)
    {
        if (turretCountText != null)
        {
            turretCountText.GetComponent<TextMeshProUGUI>().text = currentTurrets.ToString();
        }
        else
        {
            Debug.LogWarning("Turret Count Text is null");
        }
    }
    
    // Update the turret cooldown bar
    public void TurretCooldownUpdater(float fillAmount)
    {
        if (turretCooldownBar != null)
        {
            turretCooldownBar.GetComponent<Image>().fillAmount = fillAmount;
        }
        else
        {
            Debug.LogWarning("Turret Cooldown Bar is null");
        }
    }
    
    public void HungerBarUpdater(float fillAmount)
    {
        if (hungerBar != null)
        {
            hungerBar.GetComponent<Image>().fillAmount = fillAmount;
        }
        else
        {
            Debug.LogWarning("Hunger Bar is null");
        }
    }
    
    public void ThirstBarUpdater(float fillAmount)
    {
        if (thirstBar != null)
        {
            thirstBar.GetComponent<Image>().fillAmount = fillAmount;
        }
        else
        {
            Debug.LogWarning("Thirst Bar is null");
        }
    }
    
    public void EndGameScreen(bool show, string cause = "")
    {
        if (show)
        {
            endGameScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("Game Over Reason").GetComponent<TextMeshProUGUI>().text = cause;
        }
        else
        {
            endGameScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject interactionText;
    public GameObject ammoCountText;
    public GameObject turretCountText;
    
    // Start is called before the first frame update
    void Awake()
    {
        ammoCountText = GameObject.Find("Ammo Count");
        turretCountText = GameObject.Find("Turret Count");
        interactionText = GameObject.Find("Interaction Text");
        
        if (interactionText != null)
        {
            interactionText.transform.parent.gameObject.SetActive(false);
            
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
    #endregion

    #region Turret UI
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
    }
    
    public void TurretCountUpdater(string text, int? currentTurrets, int? maxTurrets)
    {
        if (turretCountText != null)
        {
            if (text != null)
            {
                turretCountText.GetComponent<TextMeshProUGUI>().text = text;
            }
            else if (maxTurrets != null)
            {
                turretCountText.GetComponent<TextMeshProUGUI>().text = currentTurrets  + "/" + maxTurrets;
            }
            else
            {
                turretCountText.GetComponent<TextMeshProUGUI>().text = currentTurrets.ToString();
            }
        }
    }
    
    #endregion
}

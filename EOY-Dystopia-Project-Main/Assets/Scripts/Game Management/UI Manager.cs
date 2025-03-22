using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject interactionPrompt;
    
    // Start is called before the first frame update
    void Start()
    {
        interactionPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    // Show the interaction prompt with the given text
    public void ShowInteractionPrompt(string text)
    {
        interactionPrompt.SetActive(true);
        interactionPrompt.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text + " : " + "[ " + GameManager.Instance.InputManager.InputMap.Player.Interact.GetBindingDisplayString() + " ]";
    }
    
    
    // Hide the interaction prompt
    public void HideInteractionPrompt()
    {
        interactionPrompt.SetActive(false);
        interactionPrompt.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "No Prompt (This is a bug...)";
    }
}


using UnityEngine;

public class Clipboard : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string InteractText => "Tutorial";
    public bool Interact(Interactor interactor)
    {
        GameManager.Instance.UIManager.TutorialScreen(true);
        
        return true;
    }
    
    public void Close()
    {
        GameManager.Instance.UIManager.TutorialScreen(false);
    }
    public void UISound(AudioClip clip)
    {
        GameManager.Instance.AudioManager.PlayUIAudio(clip);
    }

    public bool Interactable => true;
    public bool Hold { get; set; }
}

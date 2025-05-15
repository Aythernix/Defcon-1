using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Config")]
    [SerializeField] private GameObject _versionText;
    [SerializeField] private GameObject _title;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        _versionText.GetComponent<TextMeshProUGUI>().text = $"V{GameManager.Instance.GAME_VERSION}";
        _title.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.GAME_NAME;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
    
    public void Settings()
    {
        GameManager.Instance.Settings();
    }
    
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    
    public void PlayAudio(AudioClip clip)
    {
        GameManager.Instance.AudioManager.PlayUIAudio(clip);
    }
}

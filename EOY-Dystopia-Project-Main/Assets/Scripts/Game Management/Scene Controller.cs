using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private List<Transform> _sceneEnemies;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OutsideScene()
    {
        SceneManager.LoadScene("Outside Bunker");
    }

    public void InsideScene()
    {
        SceneManager.LoadScene("Inside Bunker");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Outside":
                GameManager.Instance.EnemyController.LoadEnemies();
                Debug.Log("Outside scene loaded");
                break;
            case "Inside":
                // Handle loading the inside scene
                Debug.Log("Inside scene loaded");
                break;
        }
    }

}

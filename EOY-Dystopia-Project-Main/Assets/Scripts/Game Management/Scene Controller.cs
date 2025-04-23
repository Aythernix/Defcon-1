using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Scene Loading Config")]
    [SerializeField] private float SceneLoadWaitPeriod = 0.02f;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public IEnumerator LoadScene(string sceneName)
    {
        // Before Load Logic
        Debug.Log($"Started Loading {sceneName}");

        GameManager.Instance.EventManager.IncomingSceneChange(SceneManager.GetSceneByName(sceneName));

        yield return new WaitForSeconds(SceneLoadWaitPeriod);

        // Loading Scene
        SceneManager.LoadScene(sceneName);
        Debug.Log($"Loaded {sceneName}");
    }

    public IEnumerator OutsideScene()
    {
        // Before Load Logic
        Debug.Log($"Started Loading Outside Scene");

        GameManager.Instance.EventManager.IncomingSceneChange();

        yield return new WaitForSeconds(SceneLoadWaitPeriod);

        // Loading Scene
        SceneManager.LoadScene("Outside Bunker");
        Debug.Log($"Loaded Outside Scene");
    }

    public IEnumerator InsideScene()
    {
        // Before Load Logic
        Debug.Log($"Started Loading Inside Scene");

        GameManager.Instance.EventManager.IncomingSceneChange();

        yield return new WaitForSeconds(SceneLoadWaitPeriod);

        // Loading Scene
        SceneManager.LoadScene("Inside Bunker");
        Debug.Log($"Loaded Inside Scene");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Outside Bunker":
                GameManager.Instance.EnemyController.LoadEnemies();
                Debug.Log("Outside scene loaded");
                break;
            case "Inside Bunker":
                // Handle loading the inside scene
                Debug.Log("Inside scene loaded");
                break;
        }
    }

}

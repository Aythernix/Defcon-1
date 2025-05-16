using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Scene Loading Config")]
    [SerializeField] private float SceneLoadWaitPeriod = 0.05f;
    
    // Start is called before the first frame update
    void Awake()
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

        yield return new WaitForSecondsRealtime(SceneLoadWaitPeriod);

        // Loading Scene
        SceneManager.LoadSceneAsync(sceneName);
    }

    public IEnumerator OutsideScene()
    {
        // Before Load Logic
        Debug.Log($"Started Loading Outside Scene");

        GameManager.Instance.EventManager.IncomingSceneChange(SceneManager.GetSceneByName("Outside Bunker"));

        yield return new WaitForSeconds(SceneLoadWaitPeriod);

        // Loading Scene
        SceneManager.LoadSceneAsync("Outside Bunker");
    }

    public IEnumerator InsideScene()
    {
        // Before Load Logic
        Debug.Log($"Started Loading Inside Scene");

        GameManager.Instance.EventManager.IncomingSceneChange(SceneManager.GetSceneByName("Inside Bunker"));

        yield return new WaitForSeconds(SceneLoadWaitPeriod);

        // Loading Scene
        SceneManager.LoadSceneAsync("Inside Bunker");
        Debug.Log($"Loaded Inside Scene");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Call the event for scene loaded
        GameManager.Instance.EventManager.SceneLoaded(scene);
        Debug.Log($"Scene Loaded: {scene.name}");
        switch (scene.name)
        {
            case "Outside Bunker":
                Debug.Log("Outside scene loaded");
                
                break;
            case "Inside Bunker":
                // Handle loading the inside scene
                Debug.Log("Inside scene loaded");
                break;
        }
       
    }

}

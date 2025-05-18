using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour
{
    [Header("Light Manager")]
    [SerializeField] private List<Light> _lights = new List<Light>();
    // Start is called before the first frame update
    void Start()
    {
        FindLights(SceneManager.GetActiveScene());
    }
    
    
    private void OnEnable()
    {
        GameManager.Instance.EventManager.OnSceneLoaded += FindLights;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLights(GameManager.Instance.PowerSystem.isPowerActive);
    }
    
    private void FindLights(Scene scene)
    {
        // Clear the list of lights
        _lights = new List<Light>();
        // Find all lights in the scene
        var lightsInScene = FindObjectsOfType<Light>(true);
        foreach (var light in lightsInScene)
        {
            if (!light.gameObject.CompareTag("Scene Light"))
            {
                _lights.Add(light);
            }
        }
        
        Debug.Log($"[LightManager] Total lights added to manager: {_lights.Count}");
    }
    
    private void UpdateLights(bool state)
    {
        foreach (var light in _lights)
        {
            if (light != null)
            {
                light.enabled = state;
            }
        }
    }
}

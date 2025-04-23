using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    /// <summary>
    /// Triggers the OnGameBoot event.
    /// Call this method when the game has successfully booted.
    /// </summary>

    // Event triggered when the game boots up
    public event Action OnGameBoot;
    public void GameBoot()
    {
        OnGameBoot?.Invoke();
    }

    /// <summary>
    /// Triggers the OnIncomingSceneChange event.
    /// Call this method when a scene change is about to occur.
    /// The scene parameter can be null to indicate no scene change or an unknown scene.
    /// </summary>

    // Event triggered before a scene change occurs (Scene can be nullable)
    public event Action<Scene?> OnIncomingSceneChange;
    public void IncomingSceneChange(Scene? scene = null)
    {
        OnIncomingSceneChange?.Invoke(scene);
    }
}

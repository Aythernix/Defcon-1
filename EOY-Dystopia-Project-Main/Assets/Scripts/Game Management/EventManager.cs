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
    
    /// <summary>
    /// Triggers the OnSceneLoaded event.
    /// Call this method when a scene has been successfully loaded.
    /// The scene parameter is the loaded scene.
    /// </summary>
    
    // Event triggered when a scene has been loaded
    public event Action<Scene> OnSceneLoaded;
    public void SceneLoaded(Scene scene)
    {
        OnSceneLoaded?.Invoke(scene);
    }

    /// <summary>
    /// Triggers OnTurretCooldown Event
    /// Call this method when the turret coolsdown
    /// </summary>

    public event Action<bool> OnTurretCooldown;

    public void TurretCooldown(bool state)
    {
        OnTurretCooldown?.Invoke(state);
    }
    
    /// <summary>
    /// Triggers OnPowerChange Event
    /// Call this method when the power state changes
    /// </summary>
    
    public event Action<bool> OnPowerChange;
    public void PowerChange(bool state)
    {
        OnPowerChange?.Invoke(state);
    }
    
    /// <summary>
    /// Triggers OnEmptyResource Event
    /// Call this method when any of the resources are empty
    /// </summary>
    
    public event Action<ResourceManagement.ConsumableType> OnEmptyResource;
    
    public void EmptyResourceState(ResourceManagement.ConsumableType type)
    {
        OnEmptyResource?.Invoke(type);
    }
    
    /// <summary>
    /// Triggers OnGameOver Event
    /// Call this method when the player lost the game
    /// Takes a boolean to show the end game screen and a string for the reason
    /// </summary>
    
    public event Action<bool, string> OnGameOver;
    
    public void GameOver(bool show, string reason)
    {
        OnGameOver?.Invoke(show, reason);
    }
}

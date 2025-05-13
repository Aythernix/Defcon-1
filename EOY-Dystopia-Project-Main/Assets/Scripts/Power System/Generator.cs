using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    [Header("Generator Config")]
    [SerializeField] private float _interactHoldTime = 5f;

    private float _interactTime;
    private AudioSource _audioSource;
    
    [Header("Audio Config")]
    [SerializeField] private AudioClip _generatorSound;
    [SerializeField] private AudioClip _repairSound;
    // Start is called before the first frame update
    void Start()   
    { 
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Interactable = !GameManager.Instance.PowerSystem.isPowerActive;
        
      if (GameManager.Instance.PowerSystem.isPowerActive)
      {
          if (_audioSource.isPlaying) return;
          _audioSource.clip = _generatorSound;
          _audioSource.Play();
      }
      else
      {
           _audioSource.Stop();
      }
        
    }

    public string InteractText { get; } = "Repair Generator";
    public bool Hold { get; set; } = true;
    public bool Interactable { get; set; }

    public bool Interact(Interactor interactor)
    {
        _interactTime += Time.deltaTime;
        
        GameManager.Instance.UIManager.UpdateHoldIndicator(_interactTime / _interactHoldTime);

        if (_interactTime >= _interactHoldTime)
        {
            InteractionComplete();
        }

        return true;
    }

    private void InteractionComplete()
    {
        _interactTime = 0;
        GameManager.Instance.PowerSystem.PowerOn();
        Debug.Log("Generator Completed");
    }



}

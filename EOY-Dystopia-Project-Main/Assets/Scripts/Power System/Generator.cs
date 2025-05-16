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

    private bool _isInteracting = false;
    private bool _wasInteractingLastFrame = false;

    public string InteractText { get; } = "Repair Generator";
    public bool Hold { get; set; } = true;
    public bool Interactable { get; set; }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
    }

    void Update()
    {
        Interactable = !GameManager.Instance.PowerSystem.isPowerActive;

        if (GameManager.Instance.PowerSystem.isPowerActive)
        {
            // Power is ON: play generator sound if not already playing
            if (_audioSource.clip != _generatorSound)
            {
                _audioSource.clip = _generatorSound;
                _audioSource.Play();
            }
        }
        else
        {
            // Power is OFF
            if (!_isInteracting && _wasInteractingLastFrame)
            {
                // Stopped interacting — stop repair sound
                if (_audioSource.clip == _repairSound)
                {
                    _audioSource.Stop();
                    _audioSource.clip = null;
                }
            }

            // Make sure generator sound is NOT playing
            if (_audioSource.clip == _generatorSound)
            {
                _audioSource.Stop();
                _audioSource.clip = null;
            }
        }

        // Reset tracking
        _wasInteractingLastFrame = _isInteracting;
        _isInteracting = false;
    }

    public bool Interact(Interactor interactor)
    {
        _isInteracting = true;

        _interactTime += Time.deltaTime;
        GameManager.Instance.UIManager.UpdateHoldIndicator(_interactTime / _interactHoldTime);

        // Play repair sound if not already playing
        if (_audioSource.clip != _repairSound)
        {
            _audioSource.clip = _repairSound;
            _audioSource.Play();
        }

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

    private void OnDisable()
    {
        _audioSource?.Stop();
        _isInteracting = false;
        _wasInteractingLastFrame = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class CooldownBarController : MonoBehaviour
{
    private Image _cooldownBar;
    
    [Header("Cooldown Bar Config")]
    
    [Range(0, 1f)] public float fillAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        _cooldownBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _cooldownBar.fillAmount = fillAmount;
        _cooldownBar.color = Color.Lerp(Color.green, Color.red, fillAmount);
    }
}

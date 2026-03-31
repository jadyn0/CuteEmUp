using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHealth (float Health, float MaxHealth)
    {
        slider.value = Health / MaxHealth;
    }
}

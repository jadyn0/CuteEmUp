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

    public void SetHealth (float playerHealth, float playerMaxHealth)
    {
        slider.value = playerHealth / playerMaxHealth;
    }
}

using JetBrains.Annotations;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    
    public float maxLength;
    public float length;
    public float height;

    public RectTransform healthBarRect;
    void Start()
    {
        healthBarRect.sizeDelta = new Vector2(maxLength, height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth (float playerHealth, float playerMaxHealth)
    {
        length = (playerHealth / playerMaxHealth) * maxLength;

        healthBarRect.sizeDelta = new Vector2(length, height);
    }
}

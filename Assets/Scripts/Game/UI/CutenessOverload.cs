using UnityEngine;
using UnityEngine.UI;

public class CutenessOverload : MonoBehaviour
{
    public float maxOverload;
    public float overload;
    public Slider slider;

    public Image face;
    public Sprite[] faceSprites;

    public GameObject indicator;
    public bool hasShownIndicator;

    private void Start()
    {
        slider.value = overload / maxOverload;
    }
    public void Increase(float amount)
    {
        overload += amount;

        if (overload >= maxOverload)
        {
            overload = maxOverload;
            face.sprite = faceSprites[3];
            if (!hasShownIndicator)
            {
                indicator.SetActive(true);
                hasShownIndicator = true;
            }
        }

        else if (overload >= 53f)
        {
            face.sprite = faceSprites[2];
            indicator.SetActive(false);
        }

        else if (overload >= 26f)
        {
            face.sprite = faceSprites[1];
            indicator.SetActive(false);
        }

        else
        {
            face.sprite = faceSprites[0];
            indicator.SetActive(false);
        }
        slider.value = overload / maxOverload;
    }
}

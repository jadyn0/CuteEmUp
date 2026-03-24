using UnityEngine;
using UnityEngine.UI;

public class CutenessOverload : MonoBehaviour
{
    public float maxOverload;
    public float overload;
    public Slider slider;

    public Image face;
    public Sprite[] faceSprites;

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
        }

        else if (overload >= 53f)
        {
            face.sprite = faceSprites[2];
        }

        else if (overload >= 26f)
        {
            face.sprite = faceSprites[1];
        }

        else
        {
            face.sprite = faceSprites[0];
        }
        slider.value = overload / maxOverload;
    }
}

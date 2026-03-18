using UnityEngine;
using UnityEngine.UI;

public class CutenessOverload : MonoBehaviour
{
    public float maxOverload;
    public float overload;
    public Slider slider;
    public void Increase(float amount)
    {
        overload += amount;
        if (overload > maxOverload)
        {
            overload = maxOverload;
        }
        slider.value = overload;
    }
}

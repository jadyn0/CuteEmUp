using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve animationCurve;
    bool isShaking;

    public void StartShake(float duration)
    {
        StartCoroutine(Shake(duration));
    }
    IEnumerator Shake(float duration)
    {
        if (!isShaking)
        {
            isShaking = true;
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                if (Time.timeScale <= 0)
                {
                    elapsedTime = duration;
                }
                elapsedTime += Time.deltaTime;
                float strength = animationCurve.Evaluate(elapsedTime / duration);
                transform.position = startPosition + Random.insideUnitSphere * strength;
                yield return null;
            }
            transform.position = startPosition;
            isShaking = false;
        }
        
    }
}

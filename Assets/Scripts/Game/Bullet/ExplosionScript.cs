using UnityEngine;
using System.Collections;

public class ExplosionScript: MonoBehaviour
{
    public float explodeDelay;
    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeDelay);
        Destroy(gameObject);
    }


}

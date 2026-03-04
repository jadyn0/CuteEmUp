using UnityEngine;

public class WaveController : MonoBehaviour
{
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;

    private int waveCount;
    
    void Start()
    {
        waveCount = 1;
    }
    void Update()
    {
        if (wave1.transform.childCount == 0)
        {
            Debug.Log("win");
        }
    }
}

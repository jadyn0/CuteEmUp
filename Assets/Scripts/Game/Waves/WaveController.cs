using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    private int waveCount;
    public bool isWaveHappening;
    public GameObject[] wavesList;
    public GameObject[] wavesBlockList;
    public int maxWaves;
    
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        waveCount = 1;
        isWaveHappening = false;
    }
    void Update()
    {
        if (waveCount <= maxWaves)
        {
            if (wavesList[waveCount - 1].transform.childCount == 0 && isWaveHappening == true)
            {
                waveCount += 1;
                isWaveHappening = false;
                if (waveCount <= maxWaves)
                {
                    wavesBlockList[waveCount - 1].SetActive(true);
                }
            }
        }
    }

    public void StartNextWave()
    {
        wavesList[waveCount-1].SetActive(true);
        isWaveHappening = true;
    }
}

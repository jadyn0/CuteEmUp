using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;

    private int waveCount;
    public bool isWaveHappening;
    public GameObject[] wavesList;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        waveCount = 1;
        isWaveHappening = false;
    }
    void Update()
    {

        if (wavesList[waveCount-1].transform.childCount == 0 && isWaveHappening = true)
        {
            isWaveHappening = false;
            Debug.Log("check");
        }
    }

    public void StartNextWave()
    {
        GameObject[] wavesList = {wave1, wave2, wave3};
        //Debug.Log(wavesList[0]);
        wavesList[waveCount-1].SetActive(true);
        isWaveHappening = true;
        waveCount+=1;
    }
}

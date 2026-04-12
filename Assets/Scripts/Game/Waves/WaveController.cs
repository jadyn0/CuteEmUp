using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveController : MonoBehaviour
{
    public int waveCount;
    public bool isWaveHappening;
    public GameObject[] wavesList;
    public GameObject levelBlock;
    public GameObject levelContainer;
    public GameObject nextLevelContainer;
    public int maxWaves;

    public float levelBlockDelay;
    public float nextWaveDelay;

    public bool isNewBackground;
    public SpriteRenderer background;
    public SpriteRenderer background2;
    public Sprite backgroundImage;


    void Start()
    {
        if (isNewBackground)
        {
            background.sprite = backgroundImage;
            background2.sprite = backgroundImage;
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        waveCount = 1;
        isWaveHappening = false;

        levelBlock.SetActive(true);
    }
    void Update()
    {   
        if (waveCount <= maxWaves)
        {
            //if waves all arent complete and current wave is finished, start next wave
            if (wavesList[waveCount - 1].transform.childCount == 0 && isWaveHappening == true)
            {
                waveCount += 1;
                isWaveHappening = false;
                if (waveCount <= maxWaves)
                {
                    StartCoroutine(StartNextWave());
                }
            }
        }
        else
        {
            //start next level and deactivate all the script in current one
            StartCoroutine(StartNextLevel());
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(nextWaveDelay);
        wavesList[waveCount-1].SetActive(true);
        isWaveHappening = true;
    }

    public void StartWaves()
    {
        wavesList[waveCount - 1].SetActive(true);
        isWaveHappening = true;
    }

    IEnumerator StartNextLevel()
    {
        if (nextLevelContainer != null)
        {
            yield return new WaitForSeconds(levelBlockDelay);
            nextLevelContainer.SetActive(true);
            levelContainer.SetActive(false);
        }
    }
}

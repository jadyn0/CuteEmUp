using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PvpWaveController : MonoBehaviour
{
    public int waveCount;
    public bool isLevelComplete;
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

    public GameObject levelComplete;

    public AudioClip LevelCompleteSound;
    public AudioClip LevelStartSound;

    public AudioClip levelMusic;
    public bool hasLevelMusic;
    public float musicVolume = 1f;

    public PvpWaveController otherWaveController;
    private bool nextLevelStarted;
    public bool isReadyToStart;


    void Start()
    {
        if (hasLevelMusic && MusicManager.instance != null)
        {
            MusicManager.instance.playMusic(levelMusic, musicVolume);
        }
        if (LevelStartSound != null && SoundFXManager.instance != null)
        {
            SoundFXManager.instance.PlaySoundFXClip(LevelStartSound, transform, 1f);
        }

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
        else if (!isLevelComplete)
        {
            //start next level and deactivate all the script in current one
            StartCoroutine(LevelComplete());
            isLevelComplete = true;
        }
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(nextWaveDelay);
        wavesList[waveCount - 1].SetActive(true);
        isWaveHappening = true;
    }

    public void StartWaves()
    {
        isReadyToStart = true;
        StartCoroutine(StartWavesCoroutine());
    }
    IEnumerator StartWavesCoroutine()
    {
        while (otherWaveController.isReadyToStart == false)
        {
            yield return new WaitForEndOfFrame();
        }
        wavesList[waveCount - 1].SetActive(true);
        isWaveHappening = true;
        otherWaveController.wavesList[waveCount - 1].SetActive(true);
        otherWaveController.isWaveHappening = true;
    }

    IEnumerator LevelComplete()
    {
        SoundFXManager.instance.PlaySoundFXClip(LevelCompleteSound, transform, 1f);
        yield return new WaitForSeconds(0.5f);
        levelComplete.SetActive(true);
        StartCoroutine(WaitForBothPlayers());
    }

    IEnumerator WaitForBothPlayers()
    {
        while (otherWaveController.isLevelComplete == false)
        {
            yield return new WaitForEndOfFrame();
        }
        if (!nextLevelStarted)
        {
            StartNextLevel();
            otherWaveController.StartNextLevel();
        }
        
    }
    public void StartNextLevel()
    {
        nextLevelStarted = true;
        StartCoroutine(StartNextLevelCoroutine());
    }
    IEnumerator StartNextLevelCoroutine()
    {
        if (nextLevelContainer != null)
        {
            yield return new WaitForSeconds(levelBlockDelay);
            nextLevelContainer.SetActive(true);
            levelContainer.SetActive(false);
        }
    }
}


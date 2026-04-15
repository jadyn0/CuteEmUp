using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SettingsMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;

    public Dropdown resolutionDropdown;
    public Dropdown languages;
    public Toggle autoFireToggle;
    public Toggle screenShakeToggle;
    Resolution[] resolutions;

    public Slider speedSlider;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    public AudioMixer Mixer;

    void Start()
    {
        autoFireToggle.isOn = PlayerPrefs.GetInt("AutoFire", 0) == 1 ? true : false;
        speedSlider.value = PlayerPrefs.GetFloat("PlayerSpeed", 4);

        screenShakeToggle.isOn = PlayerPrefs.GetInt("ScreenShake", 1) == 1 ? true : false;

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        Mixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20f);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        Mixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20f);

        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
        Mixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20f);
    }

    public void SetMaster(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetMusic(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        Mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
    }
    public void SetSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        Mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
    }

    public void SetPlayerSpeed(float Speed)
    {
        PlayerPrefs.SetFloat("PlayerSpeed", Speed);
    }

    public void SetAutoFire(bool isAutoFire)
    {
        PlayerPrefs.SetInt("AutoFire", isAutoFire ? 1 : 0);
    }

    public void SetScreenShake(bool isScreenShake)
    {
        PlayerPrefs.SetInt("ScreenShake", isScreenShake ? 1 : 0);
    }

    public void Back()
    {
        pauseMenu.unSettings();
    }
}

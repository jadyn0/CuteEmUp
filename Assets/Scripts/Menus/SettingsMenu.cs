using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Dropdown languages;
    public Toggle autoFireToggle;
    public bool toggleMusic;
    Resolution[] resolutions;
    void Start()
    {
        autoFireToggle.isOn = PlayerPrefs.GetInt("AutoFire") == 1 ? true : false;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetAutoFire(bool isAutoFire)
    {
        PlayerPrefs.SetInt("AutoFire", isAutoFire ? 1 : 0);
    }

    public void Back()
    {
        pauseMenu.unSettings();
    }
}

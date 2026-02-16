using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Start()
    {
        
    }
    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void Back()
    {
        pauseMenu.unSettings();
    }
}

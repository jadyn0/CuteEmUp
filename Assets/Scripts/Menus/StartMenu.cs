using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private string PvPSceneName;
    [SerializeField] private string EasySceneName;
    [SerializeField] private string HardSceneName;

    public GameObject startMenu;
    public GameObject startButton;

    public GameObject playerOptions;
    public GameObject playerOptionsButton;

    public GameObject controls;
    public GameObject controlsButton;

    public GameObject skinSelect;
    public GameObject skinSelectButton;

    public GameObject difficultySelect;
    public GameObject difficultyButton;

    private bool isPlayerOptions;
    private bool isSkinSelect;
    private bool isDifficultySelect;
    private bool isControls;

    EventSystem eventSystem;
    InputAction pauseAction;

    public AudioClip buttonSound;
    public AudioClip menuMusic;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        MusicManager.instance.playMusic(menuMusic, 1f);
        pauseAction = InputSystem.actions.FindAction("Cancel");
        eventSystem = EventSystem.current;
    }
    public void Play()
    {
        isPlayerOptions = true;
        playerOptions.SetActive(true);
        startMenu.SetActive(false);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(playerOptionsButton);
    }

    void Update()
    {
        if (pauseAction.triggered && pauseAction.ReadValue<float>() > 0f)
        {
            if (isPlayerOptions)
            {
                unPlayerOptions();
            }
            else if (isControls)
            {
                unControls();
            }
            else if (isSkinSelect)
            {
                unSkinSelect();
            }
            else if (isDifficultySelect)
            {
                unDifficultySelect();
            }
        }
    }

    public void PvE()
    {
        isSkinSelect = true;
        isPlayerOptions = false;
        //difficultySelect.SetActive(true);
        skinSelect.SetActive(true);
        playerOptions.SetActive(false);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(skinSelectButton);
    }
    public void Controls()
    {
        isControls = true;
        controls.SetActive(true);
        startMenu.SetActive(false);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(controlsButton);
    }

    public void Blue()
    {
        PlayerPrefs.SetInt("PlayerSkin", 0);

        isDifficultySelect = true;
        isSkinSelect = false;
        difficultySelect.SetActive(true);
        skinSelect.SetActive(false);
        eventSystem.SetSelectedGameObject(difficultyButton);
    }
    public void Purple()
    {
        PlayerPrefs.SetInt("PlayerSkin", 1);

        isDifficultySelect = true;
        isSkinSelect = false;
        difficultySelect.SetActive(true);
        skinSelect.SetActive(false);
        eventSystem.SetSelectedGameObject(difficultyButton);
    }

    public void PvP()
    {
        SceneManager.LoadScene(PvPSceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Easy()
    {
        SceneManager.LoadScene(EasySceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Hard()
    {
        SceneManager.LoadScene(HardSceneName);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void unControls()
    {
        isControls = false;
        controls.SetActive(false);
        startMenu.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(startButton);
    }

    public void unPlayerOptions()
    {
        isPlayerOptions = false;
        playerOptions.SetActive(false);
        startMenu.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(startButton);
    }

    public void unSkinSelect()
    {
        isSkinSelect = false;
        isPlayerOptions = true;
        skinSelect.SetActive(false);
        playerOptions.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(playerOptionsButton);
    }

    public void unDifficultySelect()
    {
        isDifficultySelect = false;
        isSkinSelect = true;
        difficultySelect.SetActive(false);
        skinSelect.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(skinSelectButton);
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void PlayButtonSound()
    {
        SoundFXManager.instance.PlaySoundFXClip(buttonSound, transform, 1f);
    }
}

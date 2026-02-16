using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName;
    public GameObject pauseContainer;
    public GameObject settingsContainer;
    public bool isPaused = false;
    public bool isSettings = false;

    public GameObject settingsButton;
    public GameObject pauseButton;
    EventSystem eventSystem;

    InputAction pauseAction;

    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Cancel");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        eventSystem = EventSystem.current;
    }
    void Update()
    {
        if (pauseAction.triggered && pauseAction.ReadValue<float>() > 0f)
        {
            if (isPaused)
            {
                if (isSettings)
                {
                    unSettings();
                }
                else
                {
                   Unpause(); 
                }
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Unpause();
    }

    public void Settings()
    {
        isSettings = true;
        settingsContainer.SetActive(true);
        pauseContainer.SetActive(false);
        eventSystem.SetSelectedGameObject(settingsButton);
    }
    public void unSettings()
    {
        isSettings = false;
        settingsContainer.SetActive(false);
        pauseContainer.SetActive(true);
        eventSystem.SetSelectedGameObject(pauseButton);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        pauseContainer.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        pauseContainer.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}

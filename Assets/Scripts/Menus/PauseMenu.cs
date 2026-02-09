using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName;
    public GameObject container;
    public bool isPaused = false;

    InputAction pauseAction;

    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Cancel");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (pauseAction.triggered && pauseAction.ReadValue<float>() > 0f)
        {
            if (isPaused)
            {
                Unpause();
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

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1;
    }

    private void Pause()
    {
        container.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        container.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}

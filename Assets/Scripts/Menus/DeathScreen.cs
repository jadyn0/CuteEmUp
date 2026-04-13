using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using static UnityEngine.InputSystem.DefaultInputActions;

public class DeathScreen : MonoBehaviour
{
    public GameObject credits;
    public GameObject creditsButton;

    public GameObject lastscreen;
    public GameObject button;
    public PauseMenu pauseMenu;

    EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(button);
        pauseMenu.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;

        if (lastscreen != null)
        {
            lastscreen.SetActive(false);
        }
    }
    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void Credits()
    {
        credits.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(creditsButton);
    }
}

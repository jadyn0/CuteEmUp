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

    public GameObject difficultySelect;
    public GameObject difficultyButton;

    private bool isPlayerOptions;
    private bool isDifficultySelect;

    EventSystem eventSystem;
    InputAction pauseAction;

    void Start()
    {
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
            else if (isDifficultySelect)
            {
                unDifficultySelect(); 
            }
        }
    }

    public void PvE()
    {
        isDifficultySelect = true;
        isPlayerOptions = false;
        difficultySelect.SetActive(true);
        playerOptions.SetActive(false);
        //eventSystem.SetSelectedGameObject(null);
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

    public void unPlayerOptions()
    {
        isPlayerOptions = false;
        playerOptions.SetActive(false);
        startMenu.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(startButton);
    }

    public void unDifficultySelect()
    {
        isDifficultySelect = false;
        isPlayerOptions = true;
        difficultySelect.SetActive(false);
        playerOptions.SetActive(true);
        //eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(playerOptionsButton);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

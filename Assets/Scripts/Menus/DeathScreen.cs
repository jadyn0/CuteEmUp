using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private string lastScene = "HardScene";
    void Start()
    {
        if (StartMenu.Instance != null)
        {
            lastScene = StartMenu.Instance.lastScene;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(lastScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

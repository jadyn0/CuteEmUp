using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (StartMenu.Instance != null)
        {
            
        }
    }

    public void Restart()
    {
        if (StartMenu.Instance.lastScene == null)
        {
            SceneManager.LoadScene("HardScene");
        }
        else
        {
            SceneManager.LoadScene(StartMenu.Instance.lastScene);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private string DeathSceneName;
    public HealthBar healthbar;
    public PauseMenu pauseMenu;

    public float playerHealth;
    public float maxHealth;
    public bool isDead;
    public GameObject deathScreenContainer;
    public GameObject deathButton;
    EventSystem eventSystem;

    public CameraShake cameraShake;

    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip healSound;
    void Start()
    {
        playerHealth = maxHealth;
        eventSystem = EventSystem.current;
    }

    public void Hit(float damage)
    {
        SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 1f);
        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1 ? true : false)
        {
            cameraShake.StartShake(0.3f);
        }
        
        playerHealth -= damage;

        healthbar.SetHealth(playerHealth, maxHealth);

        if (playerHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float healAmount)
    {
        SoundFXManager.instance.PlaySoundFXClip(healSound, transform, 1f);
        if (playerHealth < maxHealth)
        {
            playerHealth += healAmount;
            if (playerHealth > maxHealth)
            {
                playerHealth = maxHealth;
            }
            healthbar.SetHealth(playerHealth, maxHealth);
        }
    }
    public void Death()
    {
        SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
        isDead = true;
        deathScreenContainer.SetActive(true);
        eventSystem.SetSelectedGameObject(deathButton);
        pauseMenu.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}

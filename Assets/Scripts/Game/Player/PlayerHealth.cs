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
    public ExplosionScript explosion;

    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip healSound;

    public bool isMultiplayer = false;
    public PlayerHealth otherPlayer;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer turretRenderer;
    void Start()
    {
        playerHealth = maxHealth;
        eventSystem = EventSystem.current;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (isMultiplayer)
            {
                StartCoroutine(PvpDeath());
            }
            else
            {
                Death();
            }
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

    IEnumerator PvpDeath()
    {
        isDead = true;
        ExplosionScript newExplosion = Instantiate(explosion, transform.position + new Vector3 (0, 0, -2), Quaternion.identity);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        turretRenderer.color = new Color(1f, 1f, 1f, 0f);
        if (otherPlayer.isDead == false)
        {
            yield return new WaitForSeconds(1f);
            SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
            deathScreenContainer.SetActive(true);
            eventSystem.SetSelectedGameObject(deathButton);
            pauseMenu.isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            yield return null;
        }
    }
}

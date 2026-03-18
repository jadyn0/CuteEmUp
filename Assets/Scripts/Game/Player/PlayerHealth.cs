using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private string DeathSceneName;
    public HealthBar healthbar;

    public float playerHealth;
    public float maxHealth;
    void Start()
    {
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(float damage)
    {
        playerHealth -= damage;

        healthbar.SetHealth(playerHealth, maxHealth);

        if (playerHealth <= 0)
        {
            Death();
        }
    }

    public void Heal(float healAmount)
    {
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
        SceneManager.LoadScene(DeathSceneName);
    }
}

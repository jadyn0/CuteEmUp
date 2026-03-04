using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
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

        Debug.Log("Hit");
    }
}

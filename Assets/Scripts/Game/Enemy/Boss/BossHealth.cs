using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public HealthBar healthbar;
    public ExplosionScript explosion;
    public float health;
    public float maxHealth;

    public float HealthChance;

    public HeartPotion heartPotion;
    public CutenessOverload overload;
    public GameObject[] bar;
    public float overloadAmount;

    private void Start()
    {
        bar = GameObject.FindGameObjectsWithTag("Overload");
        overload = bar[0].gameObject.GetComponent<CutenessOverload>();
    }
    public void Hit(float damage)
    {
        float chance = Random.Range(0, 100);
        if (chance <= HealthChance)
        {
            HeartPotion newHeart = Instantiate(heartPotion, transform.position, Quaternion.identity);
        }
        overload.Increase(overloadAmount);
        health -= damage;
        healthbar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

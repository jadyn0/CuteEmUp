using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ExplosionScript explosion;
    public float health;

    public float HealthChance;

    public HeartPotion heartPotion;
    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        float chance = Random.Range(0, 100);
        if (chance <= HealthChance)
        {
            HeartPotion newHeart = Instantiate(heartPotion, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}

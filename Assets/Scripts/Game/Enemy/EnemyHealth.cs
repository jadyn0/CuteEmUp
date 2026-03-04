using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ExplosionScript explosion;
    public float health;
    public void Hit(float damage)
    {
        health = -damage;
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

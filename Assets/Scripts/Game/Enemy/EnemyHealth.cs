using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ExplosionScript explosion;
    public float health;

    public float HealthChance;

    public HeartPotion heartPotion;
    public CutenessOverload overload;
    public GameObject[] bar;
    public float overloadAmount;

    public bool hasHitAnimation;
    public string hitAnimation;
    private Animator animator;

    private void Start()
    {
        bar = GameObject.FindGameObjectsWithTag("Overload");
        overload = bar[0].gameObject.GetComponent<CutenessOverload>();
        animator = GetComponent<Animator>();
    }
    public void Hit(float damage, bool canDropHealth)
    {
        if (hasHitAnimation)
        {
            animator.Play(hitAnimation);
        }
        health -= damage;
        if (health <= 0)
        {
            Die(canDropHealth);
        }
    }

    private void Die(bool canDropHealth)
    {
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        float chance = Random.Range(0, 100);
        if (chance <= HealthChance && canDropHealth)
        {
            HeartPotion newHeart = Instantiate(heartPotion, transform.position, Quaternion.identity);
        }
        
        overload.Increase(overloadAmount);
        Destroy(gameObject);
    }
}

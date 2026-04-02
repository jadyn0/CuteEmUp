using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public HealthBar healthbar;
    public ExplosionScript explosion;
    public float health;
    public float maxHealth;

    public HeartPotion heartPotion;
    public CutenessOverload overload;
    public GameObject[] bar;
    public float overloadAmount;
    public bool isBelow50;
    public bool isBelow30;
    public float resistance = 1;

    private void Start()
    {
        bar = GameObject.FindGameObjectsWithTag("Overload");
        overload = bar[0].gameObject.GetComponent<CutenessOverload>();
    }
    public void Hit(float damage)
    {
        overload.Increase(overloadAmount);
        health -= damage * resistance;
        healthbar.SetHealth(health, maxHealth);
        if (health/maxHealth <= 0.5 && !isBelow50)
        {
            isBelow50 = true;
            resistance = 0.75f;
            StartCoroutine(SpawnHealth());
        }
        if (health / maxHealth <= 0.30 && !isBelow30)
        {
            isBelow30 = true;
            resistance = 0.5f;
            StartCoroutine(SpawnHealth());
        }
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

    IEnumerator SpawnHealth()
    {
        float heartAmount = Random.Range(3, 6);
        for (int i = 0; i < heartAmount; i++)
        {
            float heartOffset = Random.Range(0, 15);
            HeartPotion newHeart = Instantiate(heartPotion, transform.position + new Vector3(heartOffset/10, 0, 0), Quaternion.identity);
            yield return new WaitForEndOfFrame();
        }
    }
}

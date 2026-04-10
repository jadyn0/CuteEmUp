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
    public Boss boss;
    public Score score;
    public float overloadAmount;
    public int scoreAmount;
    public bool isBelow50;
    public bool isBelow30;
    public float resistance = 1;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        GameObject bar = GameObject.FindGameObjectWithTag("Overload");
        overload = bar.gameObject.GetComponent<CutenessOverload>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boss = gameObject.GetComponent<Boss>();

        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score"); ;
        score = scoreObject.GetComponent<Score>();
    }
    public void Hit(float damage)
    {
        StartCoroutine(HitFlash());
        overload.Increase(overloadAmount);
        health -= damage * resistance;
        healthbar.SetHealth(health, maxHealth);
        if (health/maxHealth <= 0.5 && !isBelow50)
        {
            isBelow50 = true;
            resistance = 0.5f;
            boss.delayUpperBound = boss.delayUpperBound * 0.5f;
            StartCoroutine(SpawnHealth());
        }
        if (health / maxHealth <= 0.30 && !isBelow30)
        {
            isBelow30 = true;
            resistance = 0.2f;
            boss.delayUpperBound = boss.delayUpperBound * 0.3f;
            StartCoroutine(SpawnHealth());
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        score.Increase(scoreAmount);
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator SpawnHealth()
    {
        float heartAmount = Random.Range(1, 2);
        for (int i = 0; i < heartAmount; i++)
        {
            float heartOffset = Random.Range(0, 15);
            HeartPotion newHeart = Instantiate(heartPotion, transform.position + new Vector3(heartOffset/10, 0, 0), Quaternion.identity);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator HitFlash()
    {
        spriteRenderer.color = new Color(1f, 0.7f, 0.7f, 1f);
        yield return new WaitForSeconds(0.135f);
        spriteRenderer.color = Color.white;
    }
}

using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public HealthBar healthbar;
    public ExplosionScript deathExplosion;
    public ExplosionScript damageExplosion;
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
    public float resistance50 = 0.5f;
    public float resistance30 = 0.2f;

    public float delay50 = 0.5f;
    public float delay30 = 0.3f;

    private SpriteRenderer spriteRenderer;

    public AudioClip damageSound;
    public AudioClip deathSound;

    private void Start()
    {
        GameObject bar = GameObject.FindGameObjectWithTag("Overload");
        overload = bar.gameObject.GetComponent<CutenessOverload>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boss = gameObject.GetComponent<Boss>();

        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score"); ;
        score = scoreObject.GetComponent<Score>();
    }
    public void Hit(float damage, Vector3 bulletPosition)
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSound, transform, 1f);
        ExplosionScript newExplosion = Instantiate(damageExplosion, bulletPosition + new Vector3(0, 0, -5 ), Quaternion.identity);
        StartCoroutine(HitFlash());
        overload.Increase(overloadAmount);
        health -= damage * resistance;
        healthbar.SetHealth(health, maxHealth);
        if (health/maxHealth <= 0.5 && !isBelow50)
        {
            isBelow50 = true;
            resistance = resistance50;
            boss.delayUpperBound = boss.delayUpperBound * delay50;
            StartCoroutine(SpawnHealth());
        }
        if (health / maxHealth <= 0.30 && !isBelow30)
        {
            isBelow30 = true;
            resistance = resistance30;
            boss.delayUpperBound = boss.delayUpperBound * delay30;
            StartCoroutine(SpawnHealth());
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.tag != "Boss")
            {
                Destroy(child.gameObject);
            }
        }
        SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
        score.Increase(scoreAmount);
        ExplosionScript newExplosion = Instantiate(deathExplosion, transform.position, Quaternion.identity);
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

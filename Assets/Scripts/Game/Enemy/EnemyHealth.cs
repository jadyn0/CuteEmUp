using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyHealth : MonoBehaviour
{
    public ExplosionScript deathExplosion;
    public ExplosionScript damageExplosion;
    public float health;

    public float HealthChance;

    public HeartPotion heartPotion;
    public CutenessOverload overload;
    public Score score;
    public float overloadAmount;
    public int scoreAmount;

    public bool hasHitAnimation;
    private SpriteRenderer spriteRenderer;

    public AudioClip damageSound;

    private void Start()
    {
        GameObject bar = GameObject.FindGameObjectWithTag("Overload");
        overload = bar.gameObject.GetComponent<CutenessOverload>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        GameObject scoreObject = GameObject.FindGameObjectWithTag("Score"); ;
        score = scoreObject.GetComponent<Score>();
    }
    public void Hit(float damage, bool canDropHealth, Vector3 bulletPosition)
    {
        SoundFXManager.instance.PlaySoundFXClip(damageSound, transform, 1f);
        if (hasHitAnimation)
        {
            ExplosionScript newExplosion = Instantiate(damageExplosion, bulletPosition + new Vector3(0, 0, -5 ), Quaternion.identity);
            StartCoroutine(HitFlash());
        }
        health -= damage;
        if (health <= 0)
        {
            Die(canDropHealth);
        }
    }

    private void Die(bool canDropHealth)
    {
        ExplosionScript newExplosion = Instantiate(deathExplosion, transform.position, Quaternion.identity);
        float chance = Random.Range(0, 100);
        if (chance <= HealthChance && canDropHealth)
        {
            HeartPotion newHeart = Instantiate(heartPotion, transform.position, Quaternion.identity);
        }
        
        overload.Increase(overloadAmount);
        score.Increase(scoreAmount);
        Destroy(gameObject);
    }

    IEnumerator HitFlash()
    {
        spriteRenderer.color = new Color(1f, 0.4f, 0.4f, 1f);
        yield return new WaitForSeconds(0.135f);
        spriteRenderer.color = Color.white;
    }
}

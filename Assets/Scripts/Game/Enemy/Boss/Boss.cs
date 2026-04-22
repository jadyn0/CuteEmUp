using UnityEngine;
using System.Collections;


public class Boss : MonoBehaviour
{
    public float delayLowerBound;
    public float delayUpperBound;

    public BossHealth bossHealth;
    public Animator animator;

    public EnemyBulletScript bullet;
    public float bulletSpeed;

    public UnicornAI unicorn;
    public ButterflyAI butterfly;
    public Rigidbody2D rb;

    public GameObject parentLayer;

    public AudioClip bossSpit;
    public AudioClip bossSummon;

    public float spitDamage = 4;
    public float collisionDamage = 6;
    public float beamDamage = 11;
    public float beamMovementSpeed = 4.5f;

    public float boundL = -3.5f;
    public float boundR = 3.5f;
    public float speed = 2;

    public float center = 0;
    public float moveAmount = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Spit()
    {
        SoundFXManager.instance.PlaySoundFXClip(bossSpit, transform, 1f);
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
        newBullet.damage = spitDamage;
        newBullet.bulletSpeed = bulletSpeed;
    }
    public void SummonButterfly(float offsetX)
    {
        SoundFXManager.instance.PlaySoundFXClip(bossSummon, transform, 1f);
        ButterflyAI newButterfly = Instantiate(butterfly, transform.position + new Vector3(offsetX, 0, -1), Quaternion.identity);
        newButterfly.isOnScreen = true;
        newButterfly.transform.parent = parentLayer.transform;
        newButterfly.wasSummoned = true;
    }

    public void SummonUnicorn(float offsetX)
    {
        SoundFXManager.instance.PlaySoundFXClip(bossSummon, transform, 1f);
        UnicornAI newUnicorn = Instantiate(unicorn, transform.position + new Vector3(offsetX, 0, -1), Quaternion.identity);
        newUnicorn.isOnScreen = true;
        newUnicorn.transform.parent = parentLayer.transform;
        newUnicorn.wasSummoned = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            rb.linearVelocityY = 0;
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(collisionDamage);
        }
    }

    public void StartSpit()
    {
        animator.SetTrigger("Spit");
    }

    public void StartCharge()
    {
        animator.SetBool("IsCharging", true);
        animator.SetTrigger("Charge");
    }
    public void StartSpawn()
    {
        float lOrR = Random.Range(0, 2);
        if (lOrR == 0)
        {
            animator.SetTrigger("SummonLeft");
        }
        else
        {
            animator.SetTrigger("SummonRight");
        }
        
    }

    public void StartBeam()
    {
        animator.SetBool("IsEyeBeaming", true);
        animator.SetTrigger("EyeBeam");
    }
    public void Attack()
    {
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        float delay = Random.Range(delayLowerBound, delayUpperBound);
        yield return new WaitForSeconds(delay);
        float attackChance = Random.Range(0, 100);

        if (bossHealth.health / bossHealth.maxHealth >= 0.5)
        {
            if (attackChance <= 60f)
            {
                StartSpit();
            }
            else
            {
                StartCharge();
            }
        }

        if ( 0.5f > bossHealth.health / bossHealth.maxHealth)
        {
            if (bossHealth.health / bossHealth.maxHealth > 0.3f)
            {
                if (attackChance <= 45f)
                {
                    StartSpit();
                }
                else
                {
                    if (attackChance < 35f)
                    {
                        StartCharge();
                    }
                    else
                    {
                        StartSpawn();
                    }
                }
            }
            else
            {
                if (attackChance <= 25f)
                {
                    StartSpit();
                }
                else
                {
                    if (attackChance < 50f)
                    {
                        StartCharge();
                    }
                    else
                    {
                        if (attackChance < 75f)
                        {
                            StartSpawn();
                        }
                        else
                        {
                            StartBeam();
                        }
                    }
                }
            }
        }
    }
}

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

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Spit()
    {
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
        newBullet.damage = 3;
        newBullet.bulletSpeed = bulletSpeed;
    }

    public void SummonButterfly(float offsetX)
    {
        ButterflyAI newButterfly = Instantiate(butterfly, transform.position + new Vector3(offsetX, 0, 0), Quaternion.identity);
        newButterfly.isOnScreen = true;
        newButterfly.transform.parent = parentLayer.transform;
    }

    public void SummonUnicorn(float offsetX)
    {
        UnicornAI newUnicorn = Instantiate(unicorn, transform.position + new Vector3(offsetX, 0, 0), Quaternion.identity);
        newUnicorn.isOnScreen = true;
        newUnicorn.transform.parent = parentLayer.transform;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.linearVelocityY = 0;
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(4);
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

        if (bossHealth.health / bossHealth.maxHealth >= 0.5)
        {
            float attackChance = Random.Range(0, 50);
            if (attackChance <= 35f)
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
                float attackChance = Random.Range(0, 70);
                if (attackChance <= 35f)
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
                        StartSpawn();
                    }
                }
            }
            else
            {
                float attackChance = Random.Range(0, 80);
                if (attackChance <= 20f)
                {
                    StartSpit();
                }
                else
                {
                    if (attackChance < 40f)
                    {
                        StartCharge();
                    }
                    else
                    {
                        if (attackChance < 60f)
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

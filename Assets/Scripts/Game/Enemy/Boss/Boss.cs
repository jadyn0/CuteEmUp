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

    public float offScreenL = -1.5f;
    public float offScreenR = 1.5f;

    public bool isMultiplayer = false;
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
        if (isMultiplayer)
        {
            ButterflyAI newButterfly3 = Instantiate(butterfly, new Vector3(offScreenR + offsetX/2, transform.position.y, transform.position.z - 1 + offsetX / 10), Quaternion.identity);
            newButterfly3.isOnScreen = true;
            newButterfly3.transform.parent = parentLayer.transform;
            newButterfly3.wasSummoned = true;
        }
        else
        {
            if (transform.position.x >= offScreenL)
            {
                ButterflyAI newButterfly1 = Instantiate(butterfly, new Vector3(offScreenR + offsetX, transform.position.y, transform.position.z - 1), Quaternion.identity);
                newButterfly1.isOnScreen = true;
                newButterfly1.transform.parent = parentLayer.transform;
                newButterfly1.wasSummoned = true;
            }
            else if (transform.position.x <= offScreenR)
            {
                ButterflyAI newButterfly2 = Instantiate(butterfly, new Vector3(offScreenL + offsetX, transform.position.y, transform.position.z - 1), Quaternion.identity);
                newButterfly2.isOnScreen = true;
                newButterfly2.transform.parent = parentLayer.transform;
                newButterfly2.wasSummoned = true;
            }
            else
            {
                ButterflyAI newButterfly = Instantiate(butterfly, transform.position + new Vector3(offsetX, 0, -1), Quaternion.identity);
                newButterfly.isOnScreen = true;
                newButterfly.transform.parent = parentLayer.transform;
                newButterfly.wasSummoned = true;
            }
        }
    }

    public void SummonUnicorn(float offsetX)
    {
        SoundFXManager.instance.PlaySoundFXClip(bossSummon, transform, 1f);
        if (isMultiplayer)
        {
            UnicornAI newUnicorn3 = Instantiate(unicorn, new Vector3(offScreenR + offsetX / 2, transform.position.y, transform.position.z - 1 + offsetX / 10), Quaternion.identity);
            newUnicorn3.isOnScreen = true;
            newUnicorn3.transform.parent = parentLayer.transform;
            newUnicorn3.wasSummoned = true;
        }
        else
        {
            if (transform.position.x >= offScreenL)
            {
                UnicornAI newUnicorn1 = Instantiate(unicorn, new Vector3(offScreenR + offsetX, transform.position.y, transform.position.z - 1), Quaternion.identity);
                newUnicorn1.isOnScreen = true;
                newUnicorn1.transform.parent = parentLayer.transform;
                newUnicorn1.wasSummoned = true;
            }
            else if (transform.position.x <= offScreenR)
            {
                UnicornAI newUnicorn2 = Instantiate(unicorn, new Vector3(offScreenL + offsetX, transform.position.y, transform.position.z - 1), Quaternion.identity);
                newUnicorn2.isOnScreen = true;
                newUnicorn2.transform.parent = parentLayer.transform;
                newUnicorn2.wasSummoned = true;
            }
            else
            {
                UnicornAI newUnicorn = Instantiate(unicorn, transform.position + new Vector3(offsetX, 0, -1), Quaternion.identity);
                newUnicorn.isOnScreen = true;
                newUnicorn.transform.parent = parentLayer.transform;
                newUnicorn.wasSummoned = true;
            }
        }
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

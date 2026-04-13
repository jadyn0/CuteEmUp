using UnityEngine;
using System.Collections;

public class ButterflyAI : MonoBehaviour
{
    public float enemySpeed;
    public float bulletSpeed;

    public string topTag;
    public string bottomTag;
    public string playerTag;

    private bool isShooting;
    public float shootChance;

    public bool isOnScreen;
    public EnemyBulletScript bullet;
    public float shootDelay;

    private PlayerHealth player;
    private EnemyHealth health;

    public LayerMask butterflyLayer;
    public bool butterflyGrace;

    private Animator animator;

    string walkAnimation;

    public AudioClip shootSound;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (transform.parent.parent != null)
        {
            if (transform.parent.parent.tag == "Level1")
            {
                walkAnimation = "ButterflyWalk";
            }
            else if (transform.parent.parent.tag == "Level2")
            {
                walkAnimation = "ButterflyWalk";
            }
            else if (transform.parent.parent.tag == "Level3")
            {
                walkAnimation = "ButterflyWalk2";
            }
            else if (transform.parent.parent.tag == "Level4")
            {
                walkAnimation = "ButterflyWalk2";
            }
            else if (transform.parent.parent.tag == "Level5")
            {
                walkAnimation = "ButterflyWalk3";
            }
            else if (transform.parent.parent.tag == "LevelBoss")
            {
                walkAnimation = "ButterflyWalk3";
            }
        }

        

        animator.Play(walkAnimation);

        health = gameObject.GetComponent<EnemyHealth>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - (Vector3.right * 0.5f), Vector2.left, 0.5f, butterflyLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + (Vector3.right * 0.5f), Vector2.right, 0.5f, butterflyLayer);
        if (hitLeft || hitRight)
        {
            butterflyGrace = true;
        }
        else
        {
            butterflyGrace = false;
        }
        
        float chance = Random.Range(0, shootChance);
        if (chance <= 1 && isOnScreen && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(topTag))
        {
            isOnScreen = true;
        }

        if (collision.gameObject.CompareTag(bottomTag))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(1);
            health.Hit(1, false, transform.position);
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        SummonBullet();
        yield return new WaitForSeconds(shootDelay);

        if (butterflyGrace)
        {
            SummonBullet();
        }
        isShooting = false;
    }

    private void SummonBullet()
    {
        SoundFXManager.instance.PlaySoundFXClip(shootSound, transform, 1f);
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
        newBullet.damage = 1;
        newBullet.bulletSpeed = bulletSpeed;
    }
}

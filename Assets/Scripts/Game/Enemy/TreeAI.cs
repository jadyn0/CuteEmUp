using UnityEngine;
using System.Collections;

public class TreeAI : MonoBehaviour
{
    public float enemySpeed;
    public float bulletSpeed;
    public float offScreenSpeed;
    private float DY;

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
    void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
        DY = offScreenSpeed;

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * DY * Time.deltaTime);
    }

    void FixedUpdate()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1, butterflyLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1, butterflyLayer);
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
            DY = enemySpeed;
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
            player.Hit(3);
            health.Hit(3, false);
        }
    }

    IEnumerator Shoot()
    {
        DY = 0;
        isShooting = true;
        yield return new WaitForSeconds(shootDelay);
        SummonBullet();

        if (butterflyGrace)
        {
            StartCoroutine(ButterflyDelay());
        }
        else
        {
            StartCoroutine(WalkDelay());
        }

    }
    IEnumerator WalkDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        isShooting = false;
        DY = enemySpeed;
    }
    IEnumerator ButterflyDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        SummonBullet();
        StartCoroutine(WalkDelay());
    }

    private void SummonBullet()
    {
        animator.Play("TreeAttack1");
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
        newBullet.damage = 3;
        newBullet.bulletSpeed = bulletSpeed;
    }
}
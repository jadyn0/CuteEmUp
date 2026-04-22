using UnityEngine;
using System.Collections;

public class FlowerAI : MonoBehaviour
{
    public float enemySpeed;
    public float bulletSpeed;
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
    string walkAnimation;
    string shootAnimation;

    public AudioClip shootSound;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        player = playerObject.GetComponent<PlayerHealth>();

        health = gameObject.GetComponent<EnemyHealth>();
        DY = enemySpeed;

        animator = GetComponent<Animator>();


        if (transform.parent.parent.tag == "Level1")
        {
            walkAnimation = "FlowerWalk";
            shootAnimation = "FlowerShoot1";
        }
        else if (transform.parent.parent.tag == "Level2")
        {
            walkAnimation = "FlowerWalk";
            shootAnimation = "FlowerShoot1";
        }
        else if (transform.parent.parent.tag == "Level3")
        {
            walkAnimation = "FlowerWalk2";
            shootAnimation = "FlowerShoot2";
        }
        else if (transform.parent.parent.tag == "Level4")
        {
            walkAnimation = "FlowerWalk2";
            shootAnimation = "FlowerShoot2";
        }
        else if (transform.parent.parent.tag == "Level5")
        {
            walkAnimation = "FlowerWalk3";
            shootAnimation = "FlowerShoot3";
        }
        else if (transform.parent.parent.tag == "LevelBoss")
        {
            walkAnimation = "FlowerWalk3";
            shootAnimation = "FlowerShoot3";
        }

        animator.Play(walkAnimation);

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
        }

        if (collision.gameObject.CompareTag(bottomTag))
        {
            player.Hit(1.5f);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(3);
            health.Hit(3, false, transform.position);
        }
    }

    IEnumerator Shoot()
    {
        int count;
        if (butterflyGrace)
        {
            count = 4;
        }
        else
        {
            count = 3;
        }
        DY = 0;
        isShooting = true;
        for (int i = count; i > 0; i--)
        {
            yield return new WaitForSeconds(shootDelay);
            SummonBullet();
        }

        StartCoroutine(WalkDelay());
    }
    IEnumerator WalkDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        isShooting = false;
        DY = enemySpeed;
    }
    private void SummonBullet()
    {
        SoundFXManager.instance.PlaySoundFXClip(shootSound, transform, 1f);
        animator.Play(shootAnimation);
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
        newBullet.damage = 3;
        newBullet.bulletSpeed = bulletSpeed;
    }
}

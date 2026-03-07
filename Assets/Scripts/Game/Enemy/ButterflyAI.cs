using UnityEngine;
using System.Collections;

public class ButterflyAI : MonoBehaviour
{
    public float enemySpeed;

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
    void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
    }
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
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
            health.Hit(1);
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
        newBullet.damage = 1;
        yield return new WaitForSeconds(shootDelay);
        isShooting = false;
    }
}

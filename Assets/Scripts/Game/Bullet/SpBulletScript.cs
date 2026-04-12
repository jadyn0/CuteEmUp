using UnityEngine;

public class SpBulletScript : MonoBehaviour
{
    public float bulletSpeed;
    private EnemyHealth enemy;
    private WaveBlockScript waveBlock;
    public ExplosionLScript explosion;

    public string topTag;
    public string enemyTag;
    public string WaveBlockTag;
    public float moveDirection;

    void Update()
    {
        move();
    }

    void move()
    {
        transform.Translate(Vector3.up * bulletSpeed * moveDirection * Time.deltaTime);
    }

    void collision()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag(topTag)) 
        { 
            Destroy(gameObject);
        } 

        if (collision.gameObject.CompareTag(enemyTag))
        {
            ExplosionLScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.Hit(5, true, transform.position);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            ExplosionLScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            BossHealth boss = collision.gameObject.GetComponent<BossHealth>();
            boss.Hit(5, transform.position);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag(WaveBlockTag))
        {
            waveBlock = collision.gameObject.GetComponent<WaveBlockScript>();
            waveBlock.Hit();
            Destroy(gameObject);
        }
    } 
}

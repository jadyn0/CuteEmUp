using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    private EnemyHealth enemy;
    private WaveBlockScript waveBlock;

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
            enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.Hit(1, true, transform.position);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            BossHealth boss = collision.gameObject.GetComponent<BossHealth>();
            boss.Hit(1, transform.position);
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

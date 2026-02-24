using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed;
    public ExplosionScript explosion;

    public string topTag;
    public string bottomTag;

    public float shootChance;

    public bool isOnScreen;
    public EnemyBulletScript bullet;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        float chance = Random.Range(0, shootChance);
        if (chance <= 1 && isOnScreen)
        {
            Shoot();
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
            Die();
        }
    }

    private void Shoot()
    {
        EnemyBulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.moveDirection = -1;
    }

    public void Hit()
    {
        Die();
    }

    private void Die()
    {
        ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

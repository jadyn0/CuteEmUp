using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed;
    public ExplosionScript explosion;

    public string topTag;
    public string bottomTag;

    public bool isOnScreen;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
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

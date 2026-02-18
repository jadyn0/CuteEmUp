using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    private EnemyAI enemy;

    public string topTag;
    public string enemyTag;
    void Update()
    {
        move();
    }

    void move()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
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
            enemy = collision.gameObject.GetComponent<EnemyAI>();
            enemy.Hit();
            Destroy(gameObject);
        }
    } 
}

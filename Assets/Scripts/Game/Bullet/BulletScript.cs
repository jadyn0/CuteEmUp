using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    private EnemyHealth enemy;

    public string topTag;
    public string enemyTag;
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
            enemy.Hit(1);
            Destroy(gameObject);
        }
    } 
}

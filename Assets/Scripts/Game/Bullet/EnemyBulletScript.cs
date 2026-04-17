using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float bulletSpeed;
    
    public PlayerHealth player;

    public string bottomTag;
    public string playerTag;
    public float moveDirection;

    public float damage;

    void Update()
    {
        move();
        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
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
        if (collision.gameObject.CompareTag(bottomTag)) 
        { 
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(damage);
            Destroy(gameObject);
        }
    } 
}

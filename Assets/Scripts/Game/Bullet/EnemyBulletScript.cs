using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float bulletSpeed;
    
    public PlayerHealth player;

    public string bottomTag;
    public string playerTag;
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
        if (collision.gameObject.CompareTag(bottomTag)) 
        { 
            Destroy(gameObject);
        } 

        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit();
            Destroy(gameObject);
        }
    } 
}

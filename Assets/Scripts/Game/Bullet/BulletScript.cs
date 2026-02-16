using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed;
    public ExplosionScript explosion;
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
        if (collision.gameObject.CompareTag("Boundary")) 
        { 
            Destroy(gameObject);
        } 

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ExplosionScript newExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    } 
}

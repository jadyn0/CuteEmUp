using UnityEngine;

public class EyeBeam : MonoBehaviour
{
    Rigidbody2D rb;
    public Boss boss;
    public Animator animator;

    public float damage = 11;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (boss != null)
        {
            transform.SetPositionAndRotation(new Vector3(boss.transform.position.x, transform.position.y, 0), transform.rotation);
        }
        else
        {
            Destroy(gameObject);
        }
        if (!animator.GetBool("IsEyeBeaming"))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            rb.linearVelocityY = 0;
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(damage);
        }
    }
}

using UnityEngine;
using System.Collections;

public class HeartPotion : MonoBehaviour
{
    public float speed;
    public string bottomTag;

    public string playerTag;

    private PlayerHealth player;
    public float healAmount;
    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(bottomTag))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
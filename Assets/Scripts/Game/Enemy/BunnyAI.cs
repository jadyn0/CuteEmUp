using UnityEngine;
using System.Collections;

public class BunnyAI : MonoBehaviour
{
    public float enemySpeed;

    public string topTag;
    public string bottomTag;

    public string playerTag;

    public bool isOnScreen;

    private PlayerHealth player;
    private EnemyHealth health;
    void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
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
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(1);
            health.Hit(1);
        }
    }
}

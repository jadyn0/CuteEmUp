using UnityEngine;
using System.Collections;

public class ExplosionLScript : MonoBehaviour
{
    public float explodeDelay;
    private EnemyHealth enemy;
    public string enemyTag;

    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeDelay);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(enemyTag))
        {
            enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.Hit(3, true, transform.position);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            BossHealth boss = collision.gameObject.GetComponent<BossHealth>();
            boss.Hit(3, transform.position);
        }
    }


}
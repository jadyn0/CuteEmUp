using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Rendering;

public class LollyAI : MonoBehaviour
{
    public float enemySpeed;

    public string topTag;
    public string bottomTag;

    public string playerTag;

    public bool isOnScreen;

    private PlayerHealth player;
    private EnemyHealth health;

    private GameObject[] playerObject;

    private Animator animator;
    private bool canRun;
    void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectsWithTag("Player");
    }
    void Update()
    {

        if (!isOnScreen)
        {
            transform.Translate(Vector3.down * 1.5f * Time.deltaTime);
        }
        else if (isOnScreen && canRun)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerObject[0].transform.position, enemySpeed * Time.deltaTime);
        }
    }

    IEnumerator EnterScreen()
    {
        animator.Play("LollyLegs");
        yield return new WaitForSeconds(0.666f);
        canRun = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(topTag))
        {
            isOnScreen = true;

            StartCoroutine(EnterScreen());
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
            player.Hit(3);
            health.Hit(1);
        }
    }
}
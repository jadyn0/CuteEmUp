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

    public GameObject playerObject;

    private Animator animator;
    private bool canRun;
    string walkAnimation;
    void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
        player = playerObject.GetComponent<PlayerHealth>();

        if (transform.parent.parent.tag == "Level1")
        {
            walkAnimation = "LollyLegs";
        }
        else if (transform.parent.parent.tag == "Level2")
        {
            walkAnimation = "LollyLegs";
        }
        else if (transform.parent.parent.tag == "Level3")
        {
            walkAnimation = "LollyLegs2";
        }
        else if (transform.parent.parent.tag == "Level4")
        {
            walkAnimation = "LollyLegs2";
        }
        else if (transform.parent.parent.tag == "Level5")
        {
            walkAnimation = "LollyLegs3";
        }
        else if (transform.parent.parent.tag == "LevelBoss")
        {
            walkAnimation = "LollyLegs3";
        }
    }
    void Update()
    {

        if (!isOnScreen)
        {
            transform.Translate(Vector3.down * 1.5f * Time.deltaTime);
        }
        else if (isOnScreen && canRun)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, enemySpeed * Time.deltaTime);
        }
    }

    IEnumerator EnterScreen()
    {
        animator.Play(walkAnimation);
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
            player.Hit(1.5f);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            player = collision.gameObject.GetComponent<PlayerHealth>();
            player.Hit(3);
            health.Hit(1, false, transform.position);
        }
    }
}
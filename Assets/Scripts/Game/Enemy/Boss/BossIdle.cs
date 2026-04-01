using UnityEngine;
using System.Collections;

public class BossIdle : StateMachineBehaviour
{
    public PauseMenu pauseMenu;
    public Boss boss;
    public float speed;
    public float direction = -1;
    public Rigidbody2D rb;
    public Transform transform;

    public float boundL;
    public float boundR;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject menuObject = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu = menuObject.gameObject.GetComponent<PauseMenu>();
        rb = animator.GetComponent<Rigidbody2D>();
        transform = animator.GetComponent<Transform>();
        boss = animator.GetComponent<Boss>();
        boss.Attack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!pauseMenu.isPaused)
        {
            if (transform.position.x <= boundL)
            {
                direction = 1;
            }
                if (transform.position.x >= boundR)
            {
                direction = -1;
            }
                transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}

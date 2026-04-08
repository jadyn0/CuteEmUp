using UnityEngine;

public class BossEyeBeaming : StateMachineBehaviour
{
    public EyeBeam eyeBeam;
    public Transform transform;
    public float beamOffsetY;
    public float speed;
    public float direction;

    public float startPos;
    public float moveAmount;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.GetComponent<Transform>();
        EyeBeam beam = Instantiate(eyeBeam, (transform.position + new Vector3(0, beamOffsetY, -1)), Quaternion.identity);
        beam.boss = animator.GetComponent<Boss>();
        beam.animator = animator;

        startPos = transform.position.x;
        if (transform.position.x >= 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (direction == -1)
        {
            if (transform.position.x <= startPos - moveAmount)
            {
                animator.SetBool("IsEyeBeaming", false);
            }
        }
        else
        {
            if (transform.position.x >= startPos + moveAmount)
            {
                animator.SetBool("IsEyeBeaming", false);
            }
        }
        transform.Translate(speed * direction * Time.deltaTime, 0, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class BossCharge : StateMachineBehaviour
{
    public float force;
    public Transform transform;
    public Rigidbody2D rb;
    public bool bottom = false;

    public AudioClip chargeSound;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundFXManager.instance.PlaySoundFXClip(chargeSound, transform, 1f);
        transform = animator.GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();

        rb.AddForceY(-force, ForceMode2D.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(rb.linearVelocityY);
        if (rb.linearVelocityY >= -0.2 && bottom == false)
        {
            rb.linearVelocityY = 0;
            rb.AddForceY(force, ForceMode2D.Impulse);
            bottom = true;
        }
        if (rb.linearVelocityY <= 0.2 && bottom == true || transform.localPosition.y >= 0 && bottom == true)
        {
            rb.linearVelocityY = 0;
            animator.SetBool("IsCharging", false);
        }
    }

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.SetLocalPositionAndRotation(new Vector3(transform.position.x, 0, transform.position.z), transform.rotation);
        bottom = false;
    }
}

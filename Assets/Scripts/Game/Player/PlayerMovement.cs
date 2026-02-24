using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    public Vector2 moveValue;
    public float playerSpeed;
    public Rigidbody2D rb;

    private Animator animator;

    public PauseMenu pauseMenu;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //sets move input
        moveValue = moveAction.ReadValue<Vector2>();

        //animations
        if (moveValue.x != 0)
        {
            //if player is walking play walking animation
            animator.SetBool("IsWalking", true);
            animator.SetFloat("InputX", moveValue.x);
            //animator.SetFloat("LastInputX", moveValue.x);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            //animator.SetFloat("InputX", moveValue.x);
        }
    }

    void FixedUpdate()
    {
        //moves player
        rb.linearVelocity = new Vector2 (moveValue.x * playerSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Boundary")) 
        { 
            Debug.Log("hit");
        } 
    } 
}

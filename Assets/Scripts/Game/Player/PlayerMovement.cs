using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    public Vector2 moveValue;
    public float playerSpeed;
    public Rigidbody2D rb;

    private Animator animator;

    public PauseMenu pauseMenu;
    public HealthBar healthbar;

    public float healthBarMultiplier;
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

        //moves healthbar
        healthbar.transform.position = new Vector3((transform.position.x * healthBarMultiplier) + 200, healthbar.transform.position.y, 0);
    }

    void FixedUpdate()
    {
        //moves player
        rb.linearVelocity = new Vector2 (moveValue.x * playerSpeed, 0);
        //healthbar.transform.position = new Vector3((transform.position.x * healthBarMultiplier) +192, healthbar.transform.position.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Boundary")) 
        { 
            Debug.Log("hit");
        } 
    } 
}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    public Vector2 moveValue;
    public float playerSpeed;
    public Rigidbody2D rb;

    public PauseMenu pauseMenu;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    void Update()
    {
        //move player
        moveValue = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * moveValue.x * playerSpeed * Time.deltaTime);
        //rb.AddRelativeForceX(moveValue.x * playerSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Boundary")) 
        { 
            Debug.Log("hit");
        } 
    } 
}

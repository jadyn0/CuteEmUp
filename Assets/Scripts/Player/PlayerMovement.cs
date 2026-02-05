using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    InputAction shootAction;
    public Vector2 moveValue;
    public float speed;
    public Vector2 mousePos;

    public BulletScript bullet;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
    }
    void Update()
    {
        //move player
        moveValue = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * moveValue.x * speed * Time.deltaTime);
        
        if (shootAction.triggered)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        BulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
    }
}

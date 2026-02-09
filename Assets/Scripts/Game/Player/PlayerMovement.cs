using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    InputAction moveAction;
    InputAction shootAction;
    public Vector2 moveValue;
    public float playerSpeed;
    public Vector2 mousePos;

    public BulletScript bullet;
    public float shootDelay;
    Coroutine lastRoutine = null;

    public PauseMenu pauseMenu;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
    }
    void Update()
    {
        //move player
        moveValue = moveAction.ReadValue<Vector2>();
        transform.Translate(Vector3.right * moveValue.x * playerSpeed * Time.deltaTime);
        
        if (shootAction.triggered && shootAction.ReadValue<float>() > 0f && !pauseMenu.isPaused)
        {
            lastRoutine = StartCoroutine(ShootRoutine());
        }
        if (shootAction.triggered && shootAction.ReadValue<float>() == 0f)
        {
            if (lastRoutine != null)
            {
                StopCoroutine(lastRoutine);
            }
        }
    }
    private void Shoot()
    {
        BulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
    }
    IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.CompareTag("Boundary")) 
        { 
            Debug.Log("hit");
        } 
    } 
}

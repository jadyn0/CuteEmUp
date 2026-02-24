using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    InputAction shootAction;
    public BulletScript bullet;
    public float shootDelay;
    Coroutine lastRoutine = null;
    public PauseMenu pauseMenu;

    public bool isShooting;
    private Animator animator;

    public float shootOffsetY;
    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //if player is shooting, start shooting coroutine
        if (shootAction.triggered && shootAction.ReadValue<float>() > 0f && !pauseMenu.isPaused)
        {
            isShooting = true;
            lastRoutine = StartCoroutine(ShootRoutine());
        }
        //if player has stopped, stop shooting coroutine
        if (shootAction.triggered && shootAction.ReadValue<float>() == 0f)
        {
            isShooting = false;
            if (lastRoutine != null)
            {
                StopCoroutine(lastRoutine);
            }
        }
    }

    //spawn in the bullet
    private void Shoot()
    {
        animator.Play("Turret1Shoot");
        BulletScript newBullet = Instantiate(bullet, (transform.position + new Vector3(0, shootOffsetY, 1)), Quaternion.identity);
        newBullet.moveDirection = 1;
    }

    //shooting coroutine, shoots after a delay for rapidfire 
    IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
        
    }
}
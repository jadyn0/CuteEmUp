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
    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
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
        BulletScript newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
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
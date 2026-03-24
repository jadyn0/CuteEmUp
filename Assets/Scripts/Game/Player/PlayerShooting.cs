using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    InputAction shootAction;
    InputAction spShootAction;
    public BulletScript bullet;
    public SpBulletScript spBullet;

    public float shootDelay;
    public float shortDelay;
    public float longDelay;
    public bool autoFire;
    public bool storedShoot;


    public PauseMenu pauseMenu;
    public CutenessOverload overload;

    public bool isShooting;
    private Animator animator;

    public float shootOffsetY;
    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        spShootAction = InputSystem.actions.FindAction("SpAttack");
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (autoFire)
        {
            shootDelay = longDelay;
            if (shootAction.ReadValue<float>() > 0f)
            {
                if (!isShooting)
                {
                    StartCoroutine(ShootRoutine());
                }
            }
        }
        //if player is shooting, start shooting coroutine
        else
        {
            shootDelay = shortDelay;
            if (shootAction.triggered && shootAction.ReadValue<float>() > 0f)
            {
                if (!isShooting)
                {
                    StartCoroutine(ShootRoutine());
                    storedShoot = false;
                }
                else
                {
                    storedShoot = true;
                }
            }
        }

        if (spShootAction.triggered && overload.overload == overload.maxOverload)
        {
            OverloadBomb();
            overload.Increase(-80);
        }
    }

    //spawn in the bullet
    private void Shoot()
    {
        animator.Play("Turret1Shoot");
        BulletScript newBullet = Instantiate(bullet, (transform.position + new Vector3(0, shootOffsetY, 1)), Quaternion.identity);
        newBullet.moveDirection = 1;
    }

    //shooting coroutine, delays until next shot is available
    IEnumerator ShootRoutine()
    {
        isShooting = true;
        Shoot();
        yield return new WaitForSeconds(shootDelay);
        isShooting = false;
        if (storedShoot)
        {
            StartCoroutine(ShootRoutine());
            storedShoot = false;
        }
    }

    private void OverloadBomb()
    {
        animator.Play("Turret1Shoot");
        SpBulletScript newBullet = Instantiate(spBullet, (transform.position + new Vector3(0, shootOffsetY, 1)), Quaternion.identity);
        newBullet.moveDirection = 1;
    }
}
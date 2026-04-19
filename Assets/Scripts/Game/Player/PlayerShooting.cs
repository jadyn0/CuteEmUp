using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    InputAction shootAction;
    InputAction spShootAction;
    public string shootString = "Attack";
    public string spShootString = "SpAttack";

    public BulletScript bullet;
    public SpBulletScript spBullet;

    public float shootDelay;
    public float shortDelay;
    public float longDelay;
    public bool autoFire;
    public bool storedShoot;


    public PauseMenu pauseMenu;
    public CutenessOverload overload;

    public float shootOffsetY;
    public bool isShooting;

    private Animator animator;
    public bool isMultiplayer = false;
    public string idleAnimation;
    public string shootAnimation;
    public string bombAnimation;

    
    
    void Start()
    {
        shootAction = InputSystem.actions.FindAction(shootString);
        spShootAction = InputSystem.actions.FindAction(spShootString);
        animator = GetComponent<Animator>();
        if (!isMultiplayer)
        {
            if (PlayerPrefs.GetInt("PlayerSkin") == 0)
            {
                animator.Play("Turret1Idle");
                shootAnimation = "Turret1Shoot";
                bombAnimation = "Turret1Bomb";
            }
            else
            {
                animator.Play("Turret2Idle");
                shootAnimation = "Turret2Shoot";
                bombAnimation = "Turret2Bomb";
            }
        }
        else
        {
            animator.Play(idleAnimation);
        }
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("AutoFire") == 1 ? true : false)
        {
            shootDelay = longDelay;
            if (shootAction.ReadValue<float>() > 0f && !pauseMenu.isPaused)
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
            if (shootAction.triggered && shootAction.ReadValue<float>() > 0f && !pauseMenu.isPaused)
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
        animator.Play(shootAnimation);
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
        animator.Play(bombAnimation);
        SpBulletScript newBullet = Instantiate(spBullet, (transform.position + new Vector3(0, shootOffsetY, 1)), Quaternion.identity);
        newBullet.moveDirection = 1;
        if (isMultiplayer)
        {
            newBullet.isMultiplayer = true;
        }
    }
}
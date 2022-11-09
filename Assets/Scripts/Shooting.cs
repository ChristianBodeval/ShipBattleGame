using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;
using System.Runtime.InteropServices;

public class Shooting : MonoBehaviour
{
    private bool isShootingLeft;                                                          // Tells if we are shooting left or not
    private bool isShootingRight;                                                         // Tells if we are shooting right or not
    private ShipManager shipManager;
    public ShooterGroup shooterGroupLeft;
    public ShooterGroup shooterGroupRight;
    public GameObject projectilePrefab;

    public Knockback knockbackScript;

    //KnockBack
    private float currentKnockBackForce;
    public float knockbackForce = 2f;
    public float smoothKnockbackFactor;

    private float totalDamage;

    //Hidden attributes
    private float fireRateInSeconds;                                                  // Firerate
    private float projectileSpeed;

    //Accessor attributes
    public float FireRateInSeconds { get => fireRateInSeconds; set => fireRateInSeconds = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float TotalDamage { get => totalDamage; set => totalDamage = value; }

    private void Awake()
    {
        knockbackScript = GetComponent<Knockback>();
        shipManager = GetComponent<ShipManager>();
    }

    private void Start()
    {        
        StartCoroutine(ShootingCoroutine());                                              // Starts a coroutine for shooting
    }


    //Instantiates a bullet and applies a force
    private void ShootLeft()
    {
        shooterGroupLeft.Fire(projectileSpeed, totalDamage);
    }
    private void ShootRight()
    {
        shooterGroupRight.Fire(projectileSpeed, totalDamage);
    }

    //Called when fire key(s) is pressed
    public void OnFire(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        if (context.performed && value < 0)
        {                                                                                   // context.performed means whilst the button is pressed down
            isShootingLeft = true;                                                          // .start would be when the button is first pressed and
            Debug.Log("Shot left");                                                         // .canceled would be at the release of the button
        }
        else if (context.performed && value > 0)
        {
            isShootingRight = true;
            Debug.Log("Shot right");
        }
        else
        {
            isShootingLeft = false;
            isShootingRight = false;
        } 
    }

   
    //If holding firekey shoot every fireRate
    IEnumerator ShootingCoroutine()                                                     // Coroutine called at start
    {
        while (true)                                                                    // Uses a unending while loop to check if we are shooting throughout the
        {                                                                               // game
            if (isShootingLeft)
            {
                knockbackScript.AddKnockback(Vector3.left);
                ShootLeft();
                yield return new WaitForSeconds(fireRateInSeconds);

            }
            else if (isShootingRight)
            {                                                // The same but for ShootRight();
                knockbackScript.AddKnockback(Vector3.right);
                ShootRight();
                yield return new WaitForSeconds(fireRateInSeconds);

            }
            // else return nothing
            else

                yield return null;
        }
    }

    

}


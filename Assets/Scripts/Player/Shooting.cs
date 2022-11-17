using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;
using System.Runtime.InteropServices;

public class Shooting : MonoBehaviour
{
    private bool isShootingRight;                                                          // Tells if we are shooting left or not
    private bool isShootingLeft;                                                         // Tells if we are shooting right or not
    private ShipManager shipManager;
    public ShooterGroup shooterGroupRight;
    public ShooterGroup shooterGroupLeft;
    public GameObject projectilePrefab;

    public Knockback knockbackScript;
    public bool linesLeft;
    public bool linesRight;

    float shootingInputValue;

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


    public bool isChargingUp = false;
    IEnumerator coroutine;

    //float myFloat;

    private void Awake()
    {
        knockbackScript = GetComponent<Knockback>();
        shipManager = GetComponent<ShipManager>();
    }

    private void Start()
    {        
        StartCoroutine(ShootingCoroutine());                                              // Starts a coroutine for shooting
        //coroutine = ChargeUpValue();
    }

    private void Update()
    {
        RenderLines();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("StartingCoroutine");
            StartCoroutine(coroutine);
        }
        if (Input.GetKeyUp(KeyCode.Space))

        {
            Debug.Log("StoppingCoroutine");

            StopCoroutine(coroutine);
        }
    }


    private void RenderLines()
    {
        shooterGroupRight.renderLines = linesLeft;
        shooterGroupLeft.renderLines = linesRight;

    }


    //Instantiates a bullet and applies a force
    private void ShootLeft()
    {
        shooterGroupRight.Fire(projectileSpeed, totalDamage);
    }
    private void ShootRight()
    {
        shooterGroupLeft.Fire(projectileSpeed, totalDamage);
    }

    //Called when fire key(s) is pressed
    public void OnFire(InputAction.CallbackContext context)
    {
        shootingInputValue = context.ReadValue<float>();

        if (context.performed && shootingInputValue < 0)
        {                                                                                   // context.performed means whilst the button is pressed down
            isShootingRight = true;                                                          // .start would be when the button is first pressed and
            Debug.Log("Shot left");                                                         // .canceled would be at the release of the button
        }
        else if (context.performed && shootingInputValue > 0)
        {
            isShootingLeft = true;
            Debug.Log("Shot right");
        }
        else
        {
            isShootingRight = false;
            isShootingLeft = false;
        } 
    }

    /*
    IEnumerator ChargeUpValue(float parameter)
    {
        Debug.Log("Running coroutine");
        if (!isChargingUp)
        {
            isChargingUp = true;
            float chargeUpTime = 5;
            float currentTime;
            currentTime = Time.time + chargeUpTime;

            parameter = 0;

            while (Time.time < currentTime)
            {
                yield return new WaitForSeconds(1);
                parameter += 1;
                Debug.Log("Parameter" + parameter);
            }

            isChargingUp = false;
        }
        yield return null;
    }
   */
    //If holding firekey shoot every fireRate
    IEnumerator ShootingCoroutine()                                                     // Coroutine called at start
    {
        while (true)                                                                    // Uses a unending while loop to check if we are shooting throughout the
        {                                                                               // game
            
            if (isShootingRight)
            {
                


                knockbackScript.AddKnockback(Vector3.left);

                

                ShootLeft();
                yield return new WaitForSeconds(fireRateInSeconds);

            }
            else if (isShootingLeft)
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


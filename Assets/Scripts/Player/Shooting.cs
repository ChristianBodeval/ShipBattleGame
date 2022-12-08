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
    public bool renderLinesLeft;
    public bool renderLinesRight;

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

    //ChargeUp
    public float chargeUpValueRight;
    public float chargeUpValueLeft;
    private bool canShootLeft;
    private bool canShootRight;
    public float maxRange;
    //LineColors
    public Color lineColor;
    public Color cooldownColor;
    public Color chargingColor;
    /*
    //private IEnumerator ShootLeftCoroutine;
    //private IEnumerator ShootRightCoroutine;
    //private IEnumerator ChargeUpLeftCoroutine;
    //private IEnumerator ChargeUpRightCoroutine;
    */

    private void Awake()
    {
        knockbackScript = GetComponent<Knockback>();
        shipManager = GetComponent<ShipManager>();
        /*
        ShootLeftCoroutine = ShootingCoroutine(isShootingLeft, canShootLeft, chargeUpValueLeft, ChargeUpLeftCoroutine, Vector3.left, shooterGroupLeft);
        ShootRightCoroutine = ShootingCoroutine(isShootingRight, canShootRight,chargeUpValueRight, ChargeUpRightCoroutine, Vector3.right, shooterGroupRight);
        ChargeUpLeftCoroutine = ChargeUpValueLeft();
        ChargeUpRightCoroutine = ChargeUpValueRight();
        */
    }

    private void Start()
    {
        canShootLeft = true;
        canShootRight = true;
        //StartCoroutine(ShootingCoroutineLeft());                                              // Starts a coroutine for shooting
        //StartCoroutine(ShootingCoroutineRight());                                              // Starts a coroutine for shooting
        //coroutine = ChargeUpValue();
        shooterGroupLeft.SetLinesColor(lineColor);
        shooterGroupRight.SetLinesColor(lineColor);
    }

    private void Update()
    {
        RenderLines();
    }


    private void RenderLines()
    {
        shooterGroupRight.renderLines = renderLinesRight;
        shooterGroupLeft.renderLines = renderLinesLeft;

    }


    //Instantiates a bullet and applies a force
    private void ShootLeft()
    {
        SoundManager.Instance.PlayEffects("CannonShoot");
        shooterGroupLeft.Fire(projectileSpeed, totalDamage);
    }
    private void ShootRight()
    {
        SoundManager.Instance.PlayEffects("CannonShoot");
        shooterGroupRight.Fire(projectileSpeed, totalDamage);
    }


    //Called when fire key(s) is pressed
    public void OnFire(InputAction.CallbackContext context)
    {
        shootingInputValue = context.ReadValue<float>();


        if (context.performed && shootingInputValue < 0)
        {                                                                                   // context.performed means whilst the button is pressed down
            isShootingRight = true;                                                          // .start would be when the button is first pressed and
            if(canShootRight)
                StartCoroutine(ShootingCoroutineRight());
            //StartCoroutine(ShootRightCoroutine);

                                                                                            // .canceled would be at the release of the button
        }
        else if (context.performed && shootingInputValue > 0)
        {
            isShootingLeft = true;
            if(canShootLeft)
                StartCoroutine(ShootingCoroutineLeft());
        }
        else
        {
            isShootingRight = false;
            isShootingLeft = false;
        }

    }

    
    IEnumerator ChargeUpValueRight()
    {
        chargeUpValueRight = 8f;
        while (isShootingRight)
        {
            shooterGroupRight.SetLinesColor(chargingColor);
            shooterGroupRight.Range += chargeUpValueRight * Time.deltaTime;
            yield return null;
        }
    }


    IEnumerator ChargeUpValueLeft()
    {

        chargeUpValueLeft = 8f;
        while (isShootingLeft)
        {
            shooterGroupLeft.SetLinesColor(chargingColor);
            shooterGroupLeft.Range += chargeUpValueLeft * Time.deltaTime;
            yield return null;
        }
    }
    /*
    IEnumerator ChargeUpValue(bool isShooting, ShooterGroup shooterGroup, float chargeUpValue)
    {
        chargeUpValue = 8f;
        while (isShooting)
        {
            shooterGroup.Range += chargeUpValue * Time.deltaTime;
            yield return null;
        }
    }*/




    IEnumerator ShootingCoroutineRight()                                                     // Coroutine called at OnFire
    {
        if (isShootingRight && canShootRight)
        {
            canShootRight = false;
            //TODO Check for current powerup attack then skip this step
            //Increase range, while holding down

            if(!shipManager.hasPowerUp)
                yield return StartCoroutine(ChargeUpValueRight());
                
            shooterGroupRight.SetLinesColor(cooldownColor);
            knockbackScript.AddKnockback(Vector3.left);

            ShootRight();
            //Reset range
            shooterGroupRight.Range = shipManager.range_default;
            shipManager.UpdateValues();
            yield return new WaitForSeconds(fireRateInSeconds);
                
            shooterGroupRight.SetLinesColor(lineColor);
            canShootRight = true;
        }
    }


    IEnumerator ShootingCoroutineLeft()                                                     // Coroutine called at start
    {
        //Same as shootRight but for left
        if (isShootingLeft && canShootLeft)
        {
            canShootLeft = false;
            //TODO Check for current powerup attack then skip this step
            //Increase range, while holding down
            if (!shipManager.hasPowerUp)
                yield return StartCoroutine(ChargeUpValueLeft());

            shooterGroupLeft.SetLinesColor(cooldownColor);
            knockbackScript.AddKnockback(Vector3.right);

            ShootLeft();
            //Reset range
            shooterGroupLeft.Range = shipManager.range_default;
            //TODO Write with events
            shipManager.UpdateValues();
            yield return new WaitForSeconds(fireRateInSeconds);

            shooterGroupLeft.SetLinesColor(lineColor);
            canShootLeft = true;
        }
    }
    /*
    IEnumerator ShootingCoroutine(bool isShooting, bool canShoot, float chargeUpValue, IEnumerator chargeUpCoroutine, Vector3 knockbackDirection, ShooterGroup shooterGroup)                                                     // Coroutine called at start
    {
        //Same as shootRight but for left
        if (isShooting && canShoot)
        {
            canShootLeft = false;
            //TODO Check for current powerup attack then skip this step
            //Increase range, while holding down
            yield return StartCoroutine(ChargeUpValue(isShooting, shooterGroup, chargeUpValue));

            knockbackScript.AddKnockback(knockbackDirection);
            shooterGroup.SetLinesColor(cooldownColor);
            shooterGroup.Fire(projectileSpeed, totalDamage);
            shooterGroup.Range = shipManager.range_default;

            yield return new WaitForSeconds(fireRateInSeconds);

            shooterGroup.SetLinesColor(lineColor);

            canShootLeft = true;
        }
    }
    */



}


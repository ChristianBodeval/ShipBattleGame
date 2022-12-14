using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;



public class Shooting : MonoBehaviour
{
    public PlayerInputActions inputActions;
    //public Rigidbody2D CannonBall;
    public Transform[] FireTransformsLeft;                                                // Refers to the FireTransform that are attatched to the players left
    public Transform[] FireTransformsRight;                                               // Refers to the FireTransform that are attatched to the players right
    private bool isShootingLeft;                                                          // Tells if we are shooting left or not
    private bool isShootingRight;                                                         // Tells if we are shooting right or not
    public float fireRateInSeconds = 1f;                                                  // Firerate
    private SpriteRenderer sr;
    public GameObject cannonballPrefab;
    private Movement PlayerPrefab;
    private float cannonBallScaleMultiplier;
    private ShipManager shipManager;
    private Rigidbody2D explosion;

    public float damage;
    public float speed;
    public float knockbackForce = 50;
    public float CannonBallScaleMultiplier { get => cannonBallScaleMultiplier; set => cannonBallScaleMultiplier = value; }
    public float FireRateInSeconds { get => fireRateInSeconds; set => fireRateInSeconds = value; }
    private void Start()
    {
        StartCoroutine(ShootingCoroutine());                                              // Starts a coroutine for shooting


        shipManager = GetComponent<ShipManager>();
        PlayerPrefab = GetComponent<Movement>();

        UpdateValuesFromManager();
        sr = gameObject.AddComponent<SpriteRenderer>();
        // rb2D = gameObject.AddComponent<Rigidbody2D>();
    }

    void UpdateValuesFromManager()
    {
      //  cannonBallForce = shipManager.CannonBallForce;
        fireRateInSeconds = shipManager.FireRateInSeconds;
        cannonBallScaleMultiplier = shipManager.CannonBallScaleMultiplier;
    }

    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }


    private void Update()
    {


    }
    void KnockbackLeft()
    {
        // cannonballPrefab.AddForce(transform.right * Time.deltaTime);
        //transform.position -= -transform.right * Time.deltaTime * knockbackForce;
        //explosion.AddExplosionForce(500, transform.position, 500);

    }


    //Instantiates a bullet and applies a force
    private void ShootLeft()
    {
                                                                                          // If ShootLeft() is called, then ShootCannon for all FireTransforms
        foreach (var FireTransform in FireTransformsLeft)                                 // in FireTransformLEft
        {
            ShootCannon(FireTransform);                                                                  
        }
    }
    private void ShootRight()
    {
        // If Shoot() is called, then isShooting is true. aka we shoot
        foreach (var FireTransform in FireTransformsRight)
        {
            ShootCannon(FireTransform);                                                                  
        }
    }
    void ShootCannon(Transform FireTransform)
    {
        
        GameObject cannonBallInstance = Instantiate(                                       // Makes the variable  shellInstance equal to the instantiation of the 
                                                    cannonballPrefab,                                   // CannonBall Rigidbody with the position and rotation of FireTransform
                                                    FireTransform.position,                 
                                                    FireTransform.rotation);

        cannonBallInstance.GetComponent<CanonBall>().SetVariables(damage, speed);
        
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
                {                                                                           // If we are shooting, then call ShootLeft() and wait an amount equal to fireRate
                    ShootLeft();
                    KnockbackLeft();
                    yield return new WaitForSeconds(fireRateInSeconds);

                } else if (isShootingRight){                                                // The same but for ShootRight();

                    ShootRight();
                yield return new WaitForSeconds(fireRateInSeconds);

            }
                                                                                            // else return nothing
                else
                    
                    yield return null;
            }
        }
}







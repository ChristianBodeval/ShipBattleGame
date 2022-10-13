using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class Shooting : MonoBehaviour
{
    public PlayerInputActions inputActions;
    public Rigidbody2D CannonBall;
    public Transform[] FireTransformsLeft;                                                // Refers to the FireTransform that are attatched to the players left
    public Transform[] FireTransformsRight;                                               // Refers to the FireTransform that are attatched to the players right
    private bool isShooting;                                                              // Tells if we are shooting at all
    private bool isShootingLeft;                                                          // Tells if we are shooting left or not
    private bool isShootingRight;                                                         // Tells if we are shooting right or not
    public float cannonBallForce = 10f;                                                   // The speed of the cannonball
    public float fireRateInSeconds = 1f;                                                  // Firerate

    private void Start()
    {
        StartCoroutine(ShootingCoroutine());                                              // Starts a coroutine for shooting
                                                                                       
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
        
        Rigidbody2D cannonBallInstance = Instantiate(                                       // Makes the variable  shellInstance equal to the instantiation of the 
                                                    CannonBall,                             // CannonBall Rigidbody with the position and rotation of FireTransform
                                                    FireTransform.position,                 
                                                    FireTransform.rotation)
                                                    as Rigidbody2D;
        cannonBallInstance.velocity = cannonBallForce * 1.5f * FireTransform.up;            // Gives shellInstance a velocity and and a direction
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





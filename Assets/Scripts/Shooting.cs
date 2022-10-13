using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


    public class Shooting : MonoBehaviour
    {
        public PlayerInputActions inputActions;
        public Rigidbody2D CannonBall;            
        public Transform[] FireTransforms;                                                  // Refers to the FireTransform that are attatched to the players lift and right
        private bool isShooting;                                                            // Tells if we are shooting or not
        public float cannonBallForce = 10f;                                                 // The speed of the cannonball
        public float fireRateInSeconds = 1f;                                                // Firerate
   
        private void Start()
        {
            StartCoroutine(ShootingCoroutine());                                            // Starts a coroutine for shooting
        }

        //Instantiates a bullet and applies a force
        private void Shoot()
        {
            isShooting = true;                                                              // If Shoot() is called, then isShooting is true. aka we shoot
            foreach (var FireTransform in FireTransforms)
            {
                Rigidbody2D shellInstance = Instantiate(                                    // Makes the variable  shellInstance equal to the instantiation of the 
                                                    CannonBall,                             // CannonBall sprite with the position and rotation of FireTransform
                                                    FireTransform.position,                 //
                                                    FireTransform.rotation) 
                                                    as Rigidbody2D;
                shellInstance.velocity = cannonBallForce * 1.5f *  FireTransform.up;        // Gives shellInstance a velocity and and a direction
            }
        }

        //Called when fire key(s) is pressed
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)                                                          // context.performed means whilst the button is pressed down
                isShooting = true;                                                          // .start would be when the button is first pressed and
            else                                                                            // .canceled would be at the release of the button
                isShooting = false;
        }

        //If holding firekey shoot every fireRate
        IEnumerator ShootingCoroutine()                                                     // Coroutine called at start
        {                                                                                   // 
            isShooting = false;                                                             // isShooting is false since we dont wanna shoot when player spawns
            while (true)                                                                    // Uses a unending while loop to check if we are shooting throughout the
            {                                                                               // game
                if (isShooting)
                {                                                                           // If we are shooting, then call shoot() and wait an amount equal to fireRate
                    Shoot();
                    yield return new WaitForSeconds(fireRateInSeconds);
                    
                }
                                                                                            // else return nothing
                else
                    
                    yield return null;
            }
        }

}




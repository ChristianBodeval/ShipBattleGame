using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class Shooting2 : MonoBehaviour
    {
        public PlayerInputActions inputActions;
        public Rigidbody2D CannonBall;            // Prefab of the shell.
        public Transform[] FireTransforms;           // A child of the tank where the shells are spawned.

        private string FireButton;                // The input axis that is used for launching shells.
        private float lauchForce;                 // The force that will be given to the shell when the fire button is released.
        private bool Fired;                       // Whether or not the shell has been launched with this button press.
        private bool isShooting;
        public float cannonBallForce = 10f;



        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                isShooting = true;
            }
            else
                isShooting = false;
        }

        private void Shoot()
        {
            isShooting = true;

            Debug.Log("Claa");

            foreach (Transform fireTransform in FireTransforms)
            {
                Debug.Log("On for");
                // Create an instance of the shell and store a reference to it's rigidbody.
                Rigidbody2D shellInstance = Instantiate(CannonBall, fireTransform.position, fireTransform.rotation) as Rigidbody2D;
                shellInstance.velocity = cannonBallForce * 1.5f * fireTransform.up;
            }

        }

        IEnumerator ShootingCoroutine()
        {
            isShooting = false;


            while (true)
            {
                if (isShooting)
                {
                    Shoot();
                    yield return new WaitForSeconds(1f);
                    //shoot and wait
                }


                else
                    //wait for next frame
                    yield return null;


            }
        }
    }


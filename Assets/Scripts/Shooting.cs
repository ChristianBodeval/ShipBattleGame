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
        public Rigidbody2D CannonBall;            // Prefab of the shell.
        public Transform[] FireTransforms;           // A child of the tank where the shells are spawned.

        private bool isShooting;
        public float cannonBallForce = 10f;
        public float fireRateInSeconds = 1f;
   
        private void Start()
        {
            StartCoroutine(ShootingCoroutine());
        }

        //Instantiates a bullet and applies a force
        private void Shoot()
        {
            isShooting = true;
            foreach (var FireTransform in FireTransforms)
            {
                Rigidbody2D shellInstance = Instantiate(CannonBall, FireTransform.position, FireTransform.rotation) as Rigidbody2D;
                shellInstance.velocity =cannonBallForce * 1.5f *  FireTransform.up;
            }
        }
        //Called when fire key(s) is pressed
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
                isShooting = true;
            else
                isShooting = false;
        }

        //If holding firekey shoot every fireRate
        IEnumerator ShootingCoroutine()
        {
            isShooting = false;


            while (true)
            {
                if (isShooting)
                {
                    Shoot();
                    yield return new WaitForSeconds(fireRateInSeconds);
                    //shoot and wait
                }


                else
                    //wait for next frame
                    yield return null;


            }
        }

}




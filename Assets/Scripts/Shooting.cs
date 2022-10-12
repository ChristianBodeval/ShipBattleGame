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
        public Transform FireTransform;           // A child of the tank where the shells are spawned.

        private string FireButton;                // The input axis that is used for launching shells.
        private float lauchForce;                 // The force that will be given to the shell when the fire button is released.
        private bool Fired;                       // Whether or not the shell has been launched with this button press.
        private bool isShooting;
        public float cannonBallForce = 10f;
    //  private bool isShootingLeft;
    //  private bool isShootingRight;



    private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI

        }


        public void Awake()
        {
            //inputActions = new PlayerInputActions();
            //inputActions.Player.Fire.performed += ctx => OnFire();
        }


        private void Start()
        {
        StartCoroutine(ShootingCoroutine());
      //  StartCoroutine(ShootingCoroutineRight());
    }


        private void Update()
        {

      
    /*
    // Otherwise, if the fire button is released and the shell hasn't been launched yet...
    if (Input.GetButtonUp(m_FireButton) && !m_Fired)
    {
        // ... launch the shell.
        OnFire();
    }
    */
}

        private void Shoot()
        {
        isShooting = true;

        Rigidbody2D shellInstance = Instantiate(CannonBall, FireTransform.position, FireTransform.rotation) as Rigidbody2D;
        shellInstance.velocity =cannonBallForce * 1.5f *  FireTransform.up;

    }
    /*
    private void ShootRightCannon()
    {
        isShootingRight = true;

        Rigidbody2D shellInstance = Instantiate(CannonBall, FireTransform.position, FireTransform.rotation) as Rigidbody2D;
        shellInstance.velocity = cannonBallForce * 1.5f * FireTransform.up;

    }
    */


    public void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log("Jump! " + context.phase);
            if (context.performed)
            {
            }
    
        if (context.performed)
            isShooting = true;

        else
            isShooting = false;
    
        /*
        float value = context.ReadValue<float>();

        if (context.performed && value == -1)
            isShootingLeft = true;
        else
            isShootingLeft = false;
        */
      

    }

    /*
    // Set the fired flag so only Fire is only called once.
    m_Fired = true;

    // Create an instance of the shell and store a reference to it's rigidbody.
    Rigidbody shellInstance =
        Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

    // Set the shell's velocity to the launch force in the fire position's forward direction.
    shellInstance.velocity = lauchForce * m_FireTransform.forward;

    */


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
    /*
    IEnumerator ShootingCoroutineRight()
    {
        isShootingRight = false;


        while (true)
        {
            if (isShooting)
            {
                ShootRightCannon();
                yield return new WaitForSeconds(1f);
                //shoot and wait
            }


            else
                //wait for next frame
                yield return null;


        }
    }
*/
}




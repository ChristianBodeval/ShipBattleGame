using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Complete
{
    public class Shooting : MonoBehaviour
    {
        public PlayerInputActions inputActions;
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.

        private string m_FireButton;                // The input axis that is used for launching shells.
        private float lauchForce;         // The force that will be given to the shell when the fire button is released.
        private bool m_Fired;                       // Whether or not the shell has been launched with this button press.


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




        public void OnFire(InputAction.CallbackContext context)
        {
            Debug.Log("Jump! " + context.phase);
            if (context.performed)
            {
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

        }
    }
}

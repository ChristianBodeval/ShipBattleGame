using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float StartingHealth = 100f;
    public float CurrentHealth;
    private bool Dead;



    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
        Dead = false;

    }


    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        CurrentHealth -= amount;

        if (CurrentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        Dead = true;

      

        gameObject.SetActive(false);
    }
}
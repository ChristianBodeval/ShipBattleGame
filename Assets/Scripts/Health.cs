using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float health;
    public float currentHealth;
    private bool dead;
    private ShipManager shipManager;

    private void Start()
    {
        shipManager = GetComponent<ShipManager>();
        health = shipManager.Health;
    }

    private void OnEnable()
    {
        currentHealth = health;
        dead = false;

    }

    public void TakeDamage(float amount)
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        currentHealth -= amount;

        if (currentHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        dead = true;
        gameObject.SetActive(false);
    }
}
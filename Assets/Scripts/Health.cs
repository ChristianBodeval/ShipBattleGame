using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool respawnOnDeath;
    public float startingHealth;
    public float currentHealth;
    public bool dead;
    private ShipManager shipManager;
    private Color playerColor;
    private bool canTakeDamage = true;
    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    private void Awake()
    {
        shipManager = GetComponent<ShipManager>();
    }

    private void OnEnable()
    {
        dead = false;

    }


    public void TakeDamage(float amount)
    {
        if (CanTakeDamage)
        {
            currentHealth -= amount;

            if (currentHealth <= 0f && !dead)
            {
                OnDeath();
            }
        }
    }


    public virtual void OnDeath()
    {
        dead = true;

        shipManager.Die();


        if (respawnOnDeath)
        {
            Respawn();
        }

    }

    public void Respawn()
    {
        StartCoroutine(shipManager.Revive());
        currentHealth = startingHealth;
        gameObject.SetActive(true);
    }
}
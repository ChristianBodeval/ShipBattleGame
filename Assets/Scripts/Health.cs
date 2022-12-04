using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
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

    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

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
        currentHealth -= amount;

        if (currentHealth <= 0f && !dead)
        {
            OnDeath();
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
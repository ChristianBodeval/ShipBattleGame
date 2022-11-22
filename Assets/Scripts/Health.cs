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
    private bool dead;
    private ShipManager shipManager;
    public GameObject powerUpH;

    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }


    private void Update()
    {
        Debug.Log(gameObject.name + ": " + currentHealth);
    }


    private void OnEnable()
    {
        dead = false;

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        Debug.Log("Took: " + amount + " damage");

        if (currentHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }


    public virtual void OnDeath()
    {
        dead = true;
        



        if(respawnOnDeath)
        {
            Invoke("Respawn", 2);
        }

    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        currentHealth = startingHealth;
    }
}
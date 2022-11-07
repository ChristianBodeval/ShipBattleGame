using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    private bool dead;
    private ShipManager shipManager;

    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }

    private void Start()
    {
        
        currentHealth = startingHealth;
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
        Debug.Log("DIED!");
        dead = true;
        gameObject.SetActive(false);
       
    }
}
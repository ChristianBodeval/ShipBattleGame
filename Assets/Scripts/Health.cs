using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//Standard class for health scripts. Used for Merchantships, Players and Islands, health script which inherits this.
// Handles health, what happens on death and changes color momentarity when damage is taken. 
public class Health : MonoBehaviour
{
    //Color transformation
    public SpriteRenderer spriteRenderer;
    protected Color startingColor;
    protected Color damagedColor = Color.red;
    public float startingHealth;
    public float currentHealth;

    public bool dead;
    public bool canTakeDamage;

    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }


    private void OnEnable()
    {
        dead = false;
        CanTakeDamage = true;
    }
    private void OnDisable()
    {
        dead = true;
        CanTakeDamage = false;
    }
    
    public virtual void TakeDamage(float amount)
    {
        if (CanTakeDamage)
        {
            if (currentHealth > 0)
            {
                currentHealth -= amount;
                spriteRenderer.color = damagedColor;
                Invoke("ResetColor", 0.2f);
            }

            if (currentHealth <= 0f && !dead)
            {
                OnDeath();
            }
        }
    }

    public virtual void ResetColor()
    {
        spriteRenderer.color = startingColor;
    }

    public virtual void OnDeath()
    {
        dead = true;
        CancelInvoke("ResetColor");
    }
}
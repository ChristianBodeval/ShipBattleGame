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
    public SpriteRenderer spriteRenderer;
    protected Color startingColor;
    protected bool canTakeDamage = true;
    protected bool reactToHit = true;
    protected float reactCooldown = 0.2f;
    private float respawnTime = 3f;

    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }


    private void OnEnable()
    {
        dead = false;

    }


    public virtual void TakeDamage(float amount)
    {
        if (CanTakeDamage)
        {

            if (currentHealth > 0)
            {
                currentHealth -= amount;
                spriteRenderer.color = Color.red;
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


        if (respawnOnDeath)
        {
            Invoke("Respawn", respawnTime);
        }

    }

    public virtual void Respawn()
    {
        currentHealth = startingHealth;
        gameObject.SetActive(true);
    }
}
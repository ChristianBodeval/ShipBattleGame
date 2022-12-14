using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


//Standard class for health scripts. Used for Merchantships, Players and Islands, health script which inherits this. 
public class Health : MonoBehaviour
{
    //Color transformation
    public SpriteRenderer spriteRenderer;
    protected Color startingColor;
    public float startingHealth;
    public float currentHealth;

    private float respawnTime = 3f;
    protected float reactCooldown = 0.2f;

    public bool dead;
    public bool respawnOnDeath;
    public bool canTakeDamage = true;
    protected bool reactToHit = true;

    //Accessor
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

    private void Start()
    {
        CanTakeDamage = true;
    }

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private ShipManager shipManager;

    private void Awake()
    {
        spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        startingColor = spriteRenderer.color;
        shipManager = GetComponent<ShipManager>();
    }

    private void OnEnable()
    {
        dead = false;

    }

    IEnumerator OnHit()
    {
        if (reactToHit)
        {
            reactToHit = false;

            yield return new WaitForSeconds(reactCooldown);
            ResetColor();

            reactToHit = true;
        }
    }


    public override void OnDeath()
    {
        dead = true;

        shipManager.Die();




        if (respawnOnDeath)
        {
            Respawn();
        }

    }

    public override void Respawn()
    {
        ResetColor();
        shipManager.SetDefaultValues();
        currentHealth = startingHealth;
        gameObject.SetActive(true);
    }
}
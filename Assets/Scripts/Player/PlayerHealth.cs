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

    public override void OnDeath()
    {
        dead = true;
        CancelInvoke("ResetColor");
        shipManager.Die();
        spriteRenderer.color = Color.black;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class IslandHealth : Health
{
    public GameObject treasure;
    public GameObject powerUpSpawner;
    public SpriteShapeRenderer spriteShapeRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteShapeRenderer = GetComponent<SpriteShapeRenderer>();
        spriteRenderer = null;
        startingColor = spriteShapeRenderer.color;
    }
    
    public override void TakeDamage(float amount)
    {
        if (CanTakeDamage)
        {
            if (currentHealth <= 0f && !dead)
            {
                OnDeath();
            }
            else
            {
                currentHealth -= amount;
                spriteShapeRenderer.color = Color.red;
                Invoke("ResetColor", 0.2f);
            }

        }
    }

    public override void ResetColor()
    {
        spriteShapeRenderer.color = startingColor;
    }

    public override void OnDeath ()
    {
        dead = true;
        Instantiate(powerUpSpawner, treasure.GetComponent<Transform>().position, treasure.GetComponent<Transform>().rotation);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonBall : MonoBehaviour
{
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".

    public float m_MaxDamage = 100f;                    // The amount of damage done if the explosion is centred on a tank.
    public float m_MaxLifeTime = 3f;                    // The time in seconds before the shell is removed.
    public float damage = 33.4f;
    private void Start()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy(gameObject, m_MaxLifeTime);
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius

        Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();

        PlayerHealth playerHealth = rigidbody2D.GetComponent<PlayerHealth>();

        // Deal this damage to the tank.
        playerHealth.TakeDamage(damage);


        // Destroy the canonball.
        Destroy(gameObject);
    }

  
}

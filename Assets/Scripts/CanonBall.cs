using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class CanonBall : MonoBehaviour
{
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".

    public float m_MaxLifeTime = 3f;                    // The time in seconds before the shell is removed.
    public float damage;
    public float cannonBallForce = 10f;                                                   // The speed of the cannonball
    public Rigidbody2D rb;


    private void Fire()
    {
        rb.velocity = cannonBallForce * 1.5f * transform.right;            // Gives shellInstance a velocity and and a direction
        TeleportManager.instance.AddTeleportable(this.gameObject);
                                                                            // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy(gameObject, m_MaxLifeTime);
    
    
    }

  
    public void SetVariables(float _damage, float _speed)
    {
        damage = _damage;
        cannonBallForce = _speed;
        Fire();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "CanonBall(Clone)")
        {
            return;
        }
        

        if (collision.gameObject.CompareTag("Player1"))
        {
            collision.transform.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        // Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();
        Debug.Log("HIT!");



            // Destroy the canonball.

        TeleportManager.instance.RemoveTeleportable(this.gameObject);
        Destroy(gameObject);


    }


}

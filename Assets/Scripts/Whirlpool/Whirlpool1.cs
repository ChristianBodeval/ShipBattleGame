using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Whirlpool1 : MonoBehaviour
{
    public float pullForce;
    public float rotationSpeed;
    public CircleCollider2D circleCollider2D;
    public Transform spriteHolderTransform;
    public float whirlpoolDamage;
    public float takeDamageEverySeconds;
    public float movementSlowMultiplier;

    public List<Health> healthObjects;

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteHolderTransform = this.gameObject.transform.GetChild(0); //Sprite is on a child GameObject
        InvokeRepeating("DealDamage", 0, takeDamageEverySeconds);
    }

    private void FixedUpdate()
    {
        spriteHolderTransform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime); //Rotates GameObject of the sprite
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            healthObjects.Add(collision.gameObject.GetComponent<Health>());
          
        }
           
        //If ship is in whirlpool
        Health aliveObject = collision.GetComponent<Health>();
        InvokeRepeating("DealDamage(aliveObject)", takeDamageEverySeconds, takeDamageEverySeconds);
        
        if (collision.GetComponent<ShipManager>())
        {
            Debug.Log("Called");
            collision.GetComponent<ShipManager>().MaxMovementSpeed /= movementSlowMultiplier;
        }
    }

    void DealDamage()
    {
        List<Health> tempGameObjectsToDamage;

        tempGameObjectsToDamage = healthObjects;

        Health willDie = null;

        //Added willDie since deleting Count changes when they are killed and it gives an error
        for (int i = 0; i < healthObjects.Count; i++)
        {
            if (healthObjects[i].GetComponent<Health>().currentHealth < whirlpoolDamage)
            {
                willDie = healthObjects[i];
                break;
            }
                
            healthObjects[i].TakeDamage(whirlpoolDamage);
            if (healthObjects[i].GetComponent<ShipManager>())
            {
                SoundManager.Instance.PlayEffects("WhirlPool");
            }
        }

        if(willDie != null)
        {
            if (!willDie.dead)
            {
                willDie.TakeDamage(whirlpoolDamage);
                DealDamage();
            }
        }

        //Todo use promise instead
        /*
        for (int i = 0; i < tempGameObjectsToDamage.Count; i++)
        {
            if()
            tempGameObjectsToDamage[i].TakeDamage(whirlpoolDamage);
            
                continue;
            if (tempGameObjectsToDamage[i].GetComponent<ShipManager>())
            {
                SoundManager.Instance.PlayEffects("WhirlPool");
            }
        }
        */

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If ship is in whirlpool
        if (collision.GetComponent<ShipManager>())
        {
            collision.GetComponent<ShipManager>().MaxMovementSpeed = collision.GetComponent<ShipManager>().maxMovementSpeed_default;
        }

        //Reset forces when leaving
        if (GetComponent<Movement>())
        {
            collision.gameObject.GetComponent<Movement>().currentTurnValue = 0;
            collision.gameObject.GetComponent<Movement>().currentTurnAcceleration = 0;
            collision.gameObject.GetComponent<Movement>().currentMoveValue = 1;
            Debug.Log("RGB velocity: " + collision.GetComponent<Rigidbody2D>().velocity);
        }

        if (collision.gameObject.GetComponent<Health>())
            healthObjects.Remove(collision.gameObject.GetComponent<Health>());

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        float enterTime = Time.time;
        if (collision.GetComponent<ShipManager>())
        {
            //Moves player towards the center of the circleCollider2D based how close the player is to the center. 
            float colliderRadius = circleCollider2D.radius;
            Vector3 vectorToPlayer = transform.position - collision.gameObject.transform.position;
            float distance = vectorToPlayer.magnitude;

            float step = MathF.Abs(colliderRadius - distance) / colliderRadius * 0.5f; //Step distance based on radius of collider and distance to player. The closer the player is the more drag.

            if (distance > 0.001f) //Do only when player is at the center approx.
            {
                collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, step * pullForce * Time.deltaTime);
            }
        }

    }
}

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

    private void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteHolderTransform = this.gameObject.transform.GetChild(0); //Sprite is on a child GameObject
    }

    private void FixedUpdate()
    {
        spriteHolderTransform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime); //Rotates GameObject of the sprite
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If ship is in whirlpool
        if (collision.GetComponent<ShipManager>())
        {
            collision.GetComponent<ShipManager>().MaxMovementSpeed /= 2;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        //If ship is in whirlpool
        if (collision.GetComponent<ShipManager>())
        {
            collision.GetComponent<ShipManager>().MaxMovementSpeed *= 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
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

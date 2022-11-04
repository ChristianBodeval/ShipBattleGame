using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Knockback : MonoBehaviour
{
    public float knockBackMultiplier = 1;
    public float currentKnockBackMultiplier;

    public Vector2 ballDirection;



    private void Update()
    {

        currentKnockBackMultiplier = Mathf.Lerp(currentKnockBackMultiplier, 0, 0.1f);
        if (currentKnockBackMultiplier < 0.01F)
        {
            currentKnockBackMultiplier = 0;
        }
        transform.Translate(-ballDirection * currentKnockBackMultiplier);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "CannonBall(Clone)") //If cannonball
        {
            currentKnockBackMultiplier = knockBackMultiplier;
            ballDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            Debug.Log("Ball velocity: " + ballDirection);
        }


    }

    
}

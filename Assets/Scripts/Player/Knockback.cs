using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Knockback : MonoBehaviour
{
    public float knockbackValue;
    private float smoothKnockbackFactor;
    public float currentKnockBackValue;
    public Vector3 direction;

    public float KnockbackValue { get => knockbackValue; set => knockbackValue = value; }
    public float SmoothKnockbackFactor { get => smoothKnockbackFactor; set => smoothKnockbackFactor = value; }

    private void Update()
    {
        currentKnockBackValue = Mathf.Lerp(currentKnockBackValue, 0, smoothKnockbackFactor);
        if (currentKnockBackValue < 0.01F)
            currentKnockBackValue = 0;

        transform.Translate(-direction * currentKnockBackValue);
    }
    //Gives knockback to the ship in a given direction
    public void AddKnockback(Vector2 _direction)
    {
        direction = _direction;
        currentKnockBackValue = knockbackValue;
    }
    //Gives knockback to the ship in a given direction
    public void AddKnockbackWorld(Vector2 _direction)
    {
        _direction = transform.InverseTransformPoint(_direction);
        direction = _direction;
        currentKnockBackValue = knockbackValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "CannonBall(Clone)") //If cannonball
        {
            //Where the projectile is going
            Vector2 _direction = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            AddKnockback(_direction);
        }

    }

    
}

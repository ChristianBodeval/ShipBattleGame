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

    //Applies knockback, if currenKnockBackValue is not close to zero. 
    private void Update()
    {
        currentKnockBackValue = Mathf.Lerp(currentKnockBackValue, 0, smoothKnockbackFactor);
        if (currentKnockBackValue < 0.01F)
            currentKnockBackValue = 0;

        transform.Translate(-direction * currentKnockBackValue);
    }
    //Gives knockback to the ship in a given direction
    //Used as knockback for cannonballs
    public void AddKnockback(Vector2 _direction)
    {
        direction = _direction;
        currentKnockBackValue = knockbackValue;
    }

    //Gives knockback to the ship in a given direction
    //Used as knockback for ramming.
    public void AddKnockbackWorld(Vector2 _direction)
    {
        _direction = transform.InverseTransformPoint(_direction);
        direction = _direction;
        currentKnockBackValue = knockbackValue;
    }    
}

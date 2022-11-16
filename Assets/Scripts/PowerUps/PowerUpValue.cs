using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpValue : MonoBehaviour
{
    public float value;

    public bool isPositive;
    public bool isTemporary;
    public float duration;

    public Type powerUpType;

    public enum Type { Health, Attack, Speed, NumberOfTypes };

    public ShipManager pickupShip;

    private void Start()
    {
        if (!isPositive)
        {
            value *= -1;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        float timer = Time.time + duration;
        if (collision.gameObject.GetComponent<ShipManager>())
        {
            pickupShip = collision.GetComponent<ShipManager>();

            switch (powerUpType)
            {
                case Type.Health:
                    pickupShip.CurrentHealth += value;
                    break;
                case Type.Attack:
                    pickupShip.TotalDamage += value;
                    break;
                case Type.Speed:
                    pickupShip.MaxMovementSpeed += value;
                    break;
            }
            Invoke("resetValue", duration);
            gameObject.SetActive(false);
        }
    }

    void resetValue()
    {
        if (isTemporary)
        {
            switch (powerUpType)
            {
                case Type.Health:
                    pickupShip.CurrentHealth -= value;
                    break;
                case Type.Attack:
                    pickupShip.TotalDamage -= value;
                    break;
                case Type.Speed:
                    pickupShip.MaxMovementSpeed -= value;
                    break;
            }
        }

    }

    private void Update()
    {
        
    }

}

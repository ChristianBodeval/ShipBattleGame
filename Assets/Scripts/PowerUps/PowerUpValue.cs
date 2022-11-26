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

    public bool SpawnHealth;
    public bool SpawnSpawnAttack;
    public bool SpawnSpawnSpeed;

    List<Type> pickList = new List<Type>();




    private void Start()
    {
        if (SpawnHealth)
            pickList.Add(Type.Health);
        if (SpawnSpawnAttack)
            pickList.Add(Type.Attack);
        if (SpawnSpawnSpeed)
            pickList.Add(Type.Speed);

        if (!isPositive)
        {
            value *= -1;
        }
    }

    private void SetDefaultValues()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        float timer = Time.time + duration;
        if (collision.gameObject.GetComponent<ShipManager>())
        {
            gameObject.SetActive(false);
            Debug.Log("Applying powerup: " + gameObject.name);

            pickupShip = collision.GetComponent<ShipManager>();

            switch (powerUpType)
            {
                case Type.Health:
                    pickupShip.CurrentHealth += value;
                    break;
                case Type.Attack:
                    pickupShip.TotalDamage += value;
                    pickupShip.fireOnBullets = true;
                    pickupShip.maxAngle /= 3;
                    pickupShip.Range *= 3;
                    pickupShip.FireRateInSeconds /= 3;
                    pickupShip.MaxMovementSpeed /= 2;
                    pickupShip.ProjectileSpeed *= 2;
                    pickupShip.TurnAcceleration /= 2;
                    pickupShip.MaxTurnSpeed /= 2;
                    break;
                case Type.Speed:
                    pickupShip.MaxMovementSpeed += value;
                    pickupShip.TurnAcceleration *= 2;
                    pickupShip.MaxMovementSpeed *= 1.5f;
                    break;
            }
            if (isTemporary)
                Invoke("resetValue", duration);
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
                    pickupShip.fireOnBullets = false;
                    pickupShip.maxAngle *= 3;
                    pickupShip.Range /= 3;
                    pickupShip.FireRateInSeconds *= 3;
                    pickupShip.MaxMovementSpeed *= 2;
                    pickupShip.ProjectileSpeed /= 2;
                    pickupShip.TurnAcceleration *= 2;
                    pickupShip.MaxTurnSpeed *= 2;


                    break;
                case Type.Speed:
                    pickupShip.MaxMovementSpeed -= value;
                    pickupShip.TurnAcceleration /= 2;
                    pickupShip.MaxMovementSpeed /= 1.5f;
                    break;
            }
        }


    }

}

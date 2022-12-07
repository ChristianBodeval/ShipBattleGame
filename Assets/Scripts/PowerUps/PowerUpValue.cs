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

   

    ShipManager pickUpShip;

    List<Type> pickList = new List<Type>();


    private void Start()
    {
        pickupShip = null;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If not colliding with a player ship --> do nothing
        if(!collision.GetComponent<ShipManager>())
        {
            return;
        }

        pickupShip = collision.GetComponent<ShipManager>();

        //If no powerup is active;
        if (pickupShip != null && !pickupShip.hasPowerUp)
        {
            gameObject.SetActive(false);

            Debug.Log("Applying powerup: " + gameObject.name);

            pickupShip.hasPowerUp = true;

            

            switch (powerUpType)
            {
                case Type.Health:
                    pickupShip.CurrentHealth += value;
                    SoundManager.Instance.PlayEffects("Heal");
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
                    SoundManager.Instance.PlayEffects("AttackUp");
                    break;
                case Type.Speed:
                    pickupShip.MaxMovementSpeed += value;
                    pickupShip.TurnAcceleration *= 2;
                    pickupShip.MaxMovementSpeed *= 1.5f;
                    SoundManager.Instance.PlayEffects("SpeedUp");
                    break;
            }
            if (isTemporary)
                Invoke("resetValue", duration);
            else
            {
                pickupShip.hasPowerUp = false;
            }
        }
    }

    void resetValue()
    {
        if (isTemporary)
        {
            Debug.Log("Reverting powerup: " + gameObject.name);
            pickupShip.hasPowerUp = false;
            pickupShip.ResetValues();
        }


    }

}

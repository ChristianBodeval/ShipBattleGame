using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PowerUpType { Health, Attack, Speed, Snipe, NumberOfTypes};   

public class PowerUpValue : MonoBehaviour
{   
    public float value;

    public bool isPositive;
    public bool isTemporary;
    public float duration;

    public PowerUpType powerUpType;

    
    public ShipManager pickupShip;

    ShipManager pickUpShip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If not colliding with a player ship --> do nothing
        if(!collision.GetComponent<ShipManager>())
        {
            return;
        }

        pickupShip = collision.GetComponent<ShipManager>();

        if(pickupShip != null && pickupShip.hasPowerUp)
        {
            CancelInvoke("resetValues");
            resetValues();
        }

        //If no powerup is active;
        if (pickupShip != null && !pickupShip.hasPowerUp)
        {
            gameObject.SetActive(false);
            pickupShip.hasPowerUp = true;

            switch (powerUpType)
            {
                case PowerUpType.Health:
                    SoundManager.Instance.PlayEffects("Heal");

                    //Heal only to the maxHealth
                    PlayerHealth healthScript = pickupShip.GetComponent<PlayerHealth>();

                    if (healthScript.currentHealth + value > healthScript.startingHealth)
                        healthScript.currentHealth = healthScript.startingHealth;
                    else
                        healthScript.currentHealth += value;
                    break;
                case PowerUpType.Attack:
                    pickupShip.fireOnBullets = true;
                    pickupShip.maxAngle /= 3;
                    pickupShip.Range *= 6;
                    pickupShip.FireRateInSeconds /= 7;
                    pickupShip.MaxMovementSpeed /= 3;
                    pickupShip.ProjectileSpeed *= 3;
                    pickupShip.TurnAcceleration /= 4;
                    pickupShip.MaxTurnSpeed /= 2;
                    pickupShip.SmoothTurningFactor = 0.01f;
                    SoundManager.Instance.PlayEffects("AttackUp");
                    break;
                case PowerUpType.Speed:
                    pickupShip.TurnAcceleration *= 4;
                    pickupShip.MaxTurnSpeed *= 2;
                    pickupShip.MaxMovementSpeed *= 2.5f;
                    pickupShip.SmoothTurningFactor = 0.3f;
                    SoundManager.Instance.PlayEffects("SpeedUp");
                    break;
                case PowerUpType.Snipe:
                    pickupShip.shooters = 1;
                    pickupShip.TotalDamage = 300;
                    pickupShip.fireOnBullets = true;
                    pickupShip.Range *= 8;
                    pickupShip.FireRateInSeconds *= 4;
                    pickupShip.ProjectileSpeed *= 8;
                    pickupShip.SmoothTurningFactor = 0.03f;
                    break;
            }

            pickupShip.UpdateValues();

            if (isTemporary)
                Invoke("resetValues", duration);
            else
            {
                pickupShip.hasPowerUp = false;
            }
        }
    }

    void resetValues()
    {
        pickupShip.ResetValues();
        pickupShip.hasPowerUp = false;
    }

}

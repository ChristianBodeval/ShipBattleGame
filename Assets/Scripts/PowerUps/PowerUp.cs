using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    float percentageIncrease;
    ShipManager pickUpShip = new ShipManager();

    /*

    void Use(ref float varible)
    {
        varible *= percentageIncrease;
        ref float = pickUpShip.MaxMovementSpeed

        ref decimal estValue = ref Building.GetEstimatedValue();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
    */


    public class SpeedPowerUp : PowerUp
    {

    }
}

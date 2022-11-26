using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShipHealth : Health
{
    public GameObject healthPowerUp;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        OnDeath();
    }
    public override void OnDeath()
    {
        if (CurrentHealth <= 0)
        {
            Instantiate(healthPowerUp, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}

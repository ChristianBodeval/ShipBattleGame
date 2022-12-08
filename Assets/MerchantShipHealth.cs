using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShipHealth : Health
{
    public GameObject healthPowerUp;
    // Start is called before the first frame update


    public 
    // Update is called once per frame
    void Update()
    {
        OnDeath();
    }
    public override void OnDeath()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Merchantship dead");
            Instantiate(healthPowerUp, transform.position, Quaternion.identity);
            this.enabled = false;
            //GetComponent<MerchantShip>().enabled = false;
            //GetComponent<MerchantShipHealthBar>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}

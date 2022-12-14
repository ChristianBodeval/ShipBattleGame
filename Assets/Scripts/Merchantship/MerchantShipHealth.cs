using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShipHealth : Health
{
    public GameObject healthPowerUp;
    public MerchantShip movementScript;
    // Start is called before the first frame update




    private void Start()
    {
        startingColor = spriteRenderer.color;
    }
    // Update is called once per frame
    void Update()
    {
        OnDeath();
    }
    public override void OnDeath()
    {
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            Debug.Log("Merchantship dead");
            Instantiate(healthPowerUp, transform.position, Quaternion.identity);
            movementScript.enabled = false;
            spriteRenderer.enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            //this.enabled = false;
            //GetComponent<MerchantShipHealth>().enabled = false;
            //gameObject.SetActive(false);
        }
    }
}

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

    //Instantiates healthpowerup, disables scripts, collider, sprite and children.
    public override void OnDeath()
    {
        dead = true;
        Instantiate(healthPowerUp, transform.position, Quaternion.identity);
        movementScript.enabled = false;
        spriteRenderer.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

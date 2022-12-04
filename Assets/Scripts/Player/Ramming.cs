using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramming : MonoBehaviour
{
    public GameObject particleEffect;
    GameObject usedBy;
    BoxCollider2D boxCollider2D;
    float rammingDamage;

    public float RammingDamage { get => rammingDamage; set => rammingDamage = value; }

    private void Start()
    {
        usedBy = gameObject;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!boxCollider2D.IsTouching(collision))
        {
            return;
        }
        //Do nothing on collision with own ship
        if (usedBy == collision.gameObject)
        {
            return;
        }

        //Player hit
        if (collision.gameObject.GetComponent<Health>())
        {
            //Make particles
            Instantiate(particleEffect, collision.transform.position,collision.transform.rotation);

            collision.transform.Translate((collision.gameObject.transform.position-collision.transform.position)*2);
            collision.gameObject.GetComponent<Health>().TakeDamage(rammingDamage);
        }
        //Island hit
        if (collision.gameObject.GetComponent<IslandHealth>())
        {
            //Make particles
            Instantiate(particleEffect, collision.transform.position, collision.transform.rotation);
            collision.gameObject.GetComponent<IslandHealth>().TakeDamage(rammingDamage);
        }
        //Merchantship hit
        if (collision.gameObject.GetComponent<MerchantShipHealth>())
        {
            //Make particles
            Instantiate(particleEffect, collision.transform.position, collision.transform.rotation);
            collision.gameObject.GetComponent<MerchantShipHealth>().TakeDamage(rammingDamage);
        }
    }
}

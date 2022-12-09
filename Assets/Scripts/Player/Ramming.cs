using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramming : MonoBehaviour
{
    public GameObject particleEffect;
    GameObject usedBy;
    BoxCollider2D boxCollider2D;
    float rammingDamage;
    bool isRammed;

    public float RammingDamage { get => rammingDamage; set => rammingDamage = value; }
    public bool IsRammed { get => isRammed; set => isRammed = value; }

    private void Start()
    {
        usedBy = gameObject;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!boxCollider2D.IsTouching(collision))
        {
            //IsRammed = false;
            return;
        }
        //Do nothing on collision with own ship
        if (usedBy == collision.gameObject)
        {
            return;
        }

        //Player hit
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            IsRammed = true;
            //Make particles
            Instantiate(particleEffect, collision.transform.position,collision.transform.rotation);

            collision.transform.Translate((collision.gameObject.transform.position-collision.transform.position)*2);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(rammingDamage);
            
           
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

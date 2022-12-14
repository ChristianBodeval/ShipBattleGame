using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Adds damage to objects inside trigger, which has a Health script attacthed.
//Makes {damage} every {takeDamageEverySeconds}

public class AreaOfEffectDamage : MonoBehaviour
{
    public float damage;
    public float takeDamageEverySeconds;

    public List<Health> healthObjects;

    
    private void Start()
    {
        InvokeRepeating("DealDamage", 0, takeDamageEverySeconds);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            healthObjects.Add(collision.gameObject.GetComponent<Health>());
        }
        //If ship is in whirlpool
        Health aliveObject = collision.GetComponent<Health>();
        InvokeRepeating("DealDamage(aliveObject)", takeDamageEverySeconds, takeDamageEverySeconds);
    }

    void DealDamage()
    {
        for (int i = 0; i < healthObjects.Count; i++)
        {
            healthObjects[i].TakeDamage(damage);
            SoundManager.Instance.PlayEffects("LavaDamage");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
            healthObjects.Remove(collision.gameObject.GetComponent<Health>());
    }
}

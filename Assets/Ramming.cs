using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramming : MonoBehaviour
{
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
        if (collision.gameObject.GetComponent<Health>() && usedBy != collision.gameObject)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(rammingDamage);
        }

        if (collision.gameObject.GetComponent<IslandHealth>() && usedBy != collision.gameObject)
        {
            collision.gameObject.GetComponent<IslandHealth>().TakeDamage(rammingDamage);
        }

    }
}

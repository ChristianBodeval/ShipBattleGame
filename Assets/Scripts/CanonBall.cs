using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class CanonBall : MonoBehaviour
{
    public GameObject hitParticle;
    private GameObject shotBy;
    private float damage;
    private float projectileSpeed;                                                   // The speed of the cannonball


    private Vector3 targetPosition;

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public GameObject ShotBy { get => shotBy; set => shotBy = value; }

    // Transforms to act as start and end markers for the journey.
    private Vector3 startMarker;
    private Vector3 endMarker;

    private float startTime;     // Time when the movement started.

    // Total distance between the markers.
    private float journeyLength;

    

    private void OnEnable()
    {

        endMarker = targetPosition;
        startMarker = transform.position;

        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, targetPosition);

        TeleportManager.instance.AddTeleportable(gameObject);

    }

    private void OnDisable()
    {
        TeleportManager.instance.RemoveTeleportable(gameObject);
    }

    private void Update()
    {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * projectileSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                Debug.Log("Destination reached");
                gameObject.SetActive(false);
            }     
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Hit by another player
        if (collision.GetComponent<Health>() && shotBy != collision.gameObject)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);

            //Make particles
            Instantiate(hitParticle, collision.transform.position, collision.transform.rotation);

        }
        //Hit an island
        if (collision.GetComponent<IslandHealth>() && shotBy != collision.gameObject)
        {
            collision.GetComponent<IslandHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
        }

    }



    
}




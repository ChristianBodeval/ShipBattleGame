using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class CanonBall : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public ParticleSystem fireParticle;
    private GameObject shotBy;
    private float damage;
    private float projectileSpeed;                                                   // The speed of the cannonball


    private Vector3 targetPosition;

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public GameObject ShotBy { get => shotBy; set => shotBy = value; }

    // Transforms to act as start and end markers for the journey.
    public Vector3 startMarker;
    public Vector3 endMarker;

    private float startTime;     // Time when the movement started.

    // Total distance between the markers.
    private float journeyLength;
    public bool fireOnBullet;

    public bool hitSomething;

    

    private void OnEnable()
    {
        if(fireOnBullet)
        {
            fireParticle.Play();
        }
        endMarker = targetPosition;
        startMarker = transform.position;

        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, targetPosition);



    }

    private void OnDisable()
    {
        hitSomething = false;
        if (fireOnBullet)
        {
            fireParticle.Stop();
        }
        TeleportManager.Instance.RemoveTeleportable(gameObject);
    }

    private void Update()
    {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * projectileSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            if(!hitSomething)
                transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (fractionOfJourney >= 1)
            {
                Debug.Log("Destination reached");
                gameObject.SetActive(false);
            }     
    }

    private void Deactivate()
    {
        //Stops movement on hit, so hitparticles plays on the hit position
        endMarker = transform.position;
        //Plays particles
        fireParticle.Stop();
        hitParticle.Play();


        if (!hitParticle.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Don't collide with own ship
        if(shotBy != collision.gameObject)
        {
            
            //Hit by another player
            if (collision.GetComponent<Health>() != null && shotBy != collision.gameObject)
            {
                hitSomething = true;
                collision.GetComponent<Health>().TakeDamage(damage);
                Deactivate();
            }
            //Hit an island
            if (collision.GetComponent<IslandHealth>() != null && shotBy != collision.gameObject)
            {
                hitSomething = true;
                collision.GetComponent<IslandHealth>().TakeDamage(damage);
                Deactivate();
            }

            if (collision.gameObject.transform.root.GetComponent<MerchantShipHealth>() != null && shotBy != collision.gameObject)
            {
                hitSomething = true;
                collision.gameObject.transform.root.GetComponent<MerchantShipHealth>().TakeDamage(damage);
                Deactivate();
            }

        }



    }

   




}




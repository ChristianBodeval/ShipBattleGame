using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public LayerMask m_TankMask;                        // Used to filter what the explosion affects, this should be set to "Players".

    public float m_MaxLifeTime = 3f;                    // The time in seconds before the shell is removed.
    public float damage = 33.4f;

    private Vector3 target;

    public Vector3 Target { get => target; set => target = value; }



    // Transforms to act as start and end markers for the journey.
    public Vector3 startMarker;
    public Vector3 endMarker;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {

        startMarker = transform.position;
        endMarker = target;

        speed = 5f;
            
        TeleportManager.instance.AddTeleportable(this.gameObject);
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        //Destroy(gameObject, m_MaxLifeTime);


        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }


    private void Update()
    {

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        if(fractionOfJourney >= 1)
        {
            Debug.Log("Destination reached");
            GameObject.Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "CanonBall(Clone)")
        {
            return;
        }

        if (collision.gameObject.name == "Player")
        {
            return;
        }


        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius

        Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();

        Health playerHealth = rigidbody2D.GetComponent<Health>();

        // Deal this damage to the tank.
        playerHealth.TakeDamage(damage);


            // Destroy the canonball.

        TeleportManager.instance.RemoveTeleportable(this.gameObject);
        Destroy(gameObject);


    }


}

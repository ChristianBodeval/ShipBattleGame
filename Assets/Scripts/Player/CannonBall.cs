using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public ParticleSystem fireParticle;
    //To ignore self
    private GameObject shotBy;
    private float damage;
    private float projectileSpeed;                                                   // The speed of the cannonball

    public SpriteRenderer spriteRenderer;
    private Vector3 targetPosition;

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
    public GameObject ShotBy { get => shotBy; set => shotBy = value; }

    //Transforms to act as start and end markers for the journey.
    public Vector3 startMarker;
    public Vector3 endMarker;

    private float startTime;     // Time when the movement started.

    //Total distance between the markers.
    private float journeyLength;
    public bool fireOnBullet;

    public bool hitSomething;

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        if(fireOnBullet)
        {
            fireParticle.Play();
        }
        endMarker = targetPosition;
        startMarker = transform.position;

        startTime = Time.time;

        //Calculate the journey length.
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
        //Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * projectileSpeed;

        //Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        //Set our position as a fraction of the distance between the markers.
        if(!hitSomething)
            transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

        //If journey finished and hit nothing
        if (fractionOfJourney >= 1 && !hitSomething)
        {
            gameObject.SetActive(false);
        }     
    }

    

    private IEnumerator Deactivate()
    {
        spriteRenderer.enabled = false;
        //Stops movement on hit, so hitparticles plays on the hit position
        endMarker = transform.position;
        //Plays particles
        fireParticle.Stop();
        hitParticle.Play();

        //Let the particle finish
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Don't collide with own ship
        if(shotBy != collision.gameObject)
        {
            //Hit another ship
            if (collision.GetComponent<PlayerHealth>() != null)
            {
                hitSomething = true;

                Vector3 direction = this.gameObject.transform.position - collision.gameObject.transform.position;

                collision.gameObject.GetComponent<Knockback>().AddKnockback((direction) * 0.2f);
                GameManager.Instance.SlowTime();
                SoundManager.Instance.PlayEffects("ProjectileHit");
                collision.GetComponent<Health>().TakeDamage(damage);
                StartCoroutine(Deactivate());
            }
            //Hit an island
            if (collision.GetComponent<IslandHealth>() != null)
            {
                hitSomething = true;
                SoundManager.Instance.PlayEffects("ProjectileHit");
                collision.GetComponent<IslandHealth>().TakeDamage(damage);
                StartCoroutine(Deactivate());
            }
            //Hit a merchantship
            if (collision.gameObject.transform.root.GetComponent<MerchantShipHealth>() != null)
            {
                hitSomething = true;
                SoundManager.Instance.PlayEffects("ProjectileHit");
                collision.gameObject.transform.root.GetComponent<MerchantShipHealth>().TakeDamage(damage);
                StartCoroutine(Deactivate());
            }

            //Hit volcano
            if (collision.gameObject.GetComponent<Volcano>() != null)
            {
                hitSomething = true;
                SoundManager.Instance.PlayEffects("ProjectileHit");
                StartCoroutine(Deactivate());
            }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Hit volcano
            if (collision.gameObject.GetComponent<Volcano>() != null)
            {
                hitSomething = true;
                SoundManager.Instance.PlayEffects("ProjectileHit");
                Deactivate();
            }
    }

}




using UnityEngine;

public class Ramming : MonoBehaviour
{
    public SpriteRenderer ramSprite;
    public GameObject particleEffect;
    private GameObject usedBy;
    private BoxCollider2D boxCollider2D;
    private float rammingDamage;
    public bool isRammed;

    public float RammingDamage { get => rammingDamage; set => rammingDamage = value; }
    public bool IsRammed { get => isRammed; set => isRammed = value; }

    private void Start()
    {
        usedBy = gameObject;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Debug.Log("isRammed is: " + isRammed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!boxCollider2D.IsTouching(collision))
        {
            //IsRammed = false;
            return;
        }
        //Do nothing on collision with own ship
        if (usedBy == collision.gameObject)
        {
            return;
        }

        SoundManager.Instance.PlayEffects("Ram");

        //Player hit
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            boxCollider2D.enabled = false;
            isRammed = true;
            GetComponent<Movement>().currentMoveValue = 0;
            GetComponent<Movement>().currentGear = 1;
            GameManager.Instance.SlowTime();
            GameManager.Instance.SlowTime();
            //Make particles
            Instantiate(particleEffect, collision.transform.position, collision.transform.rotation);

            Debug.Log("Ramming");


            Vector3 direction = (this.gameObject.transform.position - collision.transform.position);

            collision.gameObject.GetComponent<Knockback>().AddKnockbackWorld((direction) * 0.5f);
            //gameObject.GetComponent<Knockback>().AddKnockbackWorld((-direction)*1.5f);

            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
            //collision.gameObject.transform.Translate((collision.gameObject.transform.position-this.gameObject.transform.position)*1.5f);

            //collision.gameObject.transform.position = Vector3.Lerp(collision.gameObject.transform.position, collision.gameObject.transform.position + (collision.gameObject.transform.position - this.gameObject.transform.position)*3, 1f);

            Debug.Log("Target: " + collision.gameObject.transform);
            Debug.Log("Direction: " + direction);
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
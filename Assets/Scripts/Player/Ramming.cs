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

            //Double on purpose, to ephasize a ram
            GameManager.Instance.SlowTime();
            GameManager.Instance.SlowTime();
            //Make particles
            Instantiate(particleEffect, collision.transform.position, collision.transform.rotation);

            Vector3 direction = (this.gameObject.transform.position - collision.transform.position);
            collision.gameObject.GetComponent<Knockback>().AddKnockback((direction) * 0.5f);
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
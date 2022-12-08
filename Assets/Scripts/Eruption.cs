using JetBrains.Annotations;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eruption : MonoBehaviour
{
    private float initialTime; // Get variable that tells us the time when object spawned.

    public float eruptionTime = 20; // Set amount of time that eruption will occur.
    public float eruptionDamage = 5; //Set damage from eruption.
    public float scaleFactor = 1;
    public float opacityFactor = 1;
    public bool canTakeDamage = true;
    

    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.timeSinceLevelLoad; // Set the time.
        SoundManager.Instance.PlayEffects("Eruption");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() && canTakeDamage == true)
        {
            global::System.Object value = StartCoroutine(WaitingTime());
            collision.gameObject.GetComponent<Health>().TakeDamage(eruptionDamage);
            Debug.Log("lava damage");
        }
    }
    IEnumerator WaitingTime()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1);
        canTakeDamage = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initialTime;
        if (timeSinceInitialization == eruptionTime)
        {
            Destroy(gameObject); //Remove Eruption from game after eruptiontime ends.
        }
       

        if (timeSinceInitialization == eruptionTime-3)
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, opacityFactor/3*(timeSinceInitialization- eruptionTime)); //Make eruption fade out
        }

        Vector2 vec = new Vector2(timeSinceInitialization, timeSinceInitialization);
        transform.localScale = vec * scaleFactor;
    }
}

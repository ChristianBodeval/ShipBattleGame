using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eruption : MonoBehaviour
{
    private float initialTime; // Get variable that tells us the time when object spawned.

    public float eruptionTime = 20; // Set amount of time that eruption will occur.
    public float eruptionDamage = 1; //Set damage from eruption.
 

    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.timeSinceLevelLoad; // Set the time.
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>())
            collision.gameObject.GetComponent<Health>().TakeDamage(eruptionDamage);
        
    }
    // Update is called once per frame
    void Update()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initialTime;
        if (timeSinceInitialization == eruptionTime)
        {
            Destroy(gameObject); //Remove Eruption from game after eruptiontime ends.
        }
    
    }
}

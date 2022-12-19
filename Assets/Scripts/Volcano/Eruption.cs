using JetBrains.Annotations;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eruption : MonoBehaviour
{
    private float initialTime; // Get variable that tells us the time when object spawned.
    public float eruptionTime; // Set amount of time that eruption will occur.
    public float eruptionDamage; //Set damage from eruption.
    public float scaleFactor;
    public float opacityFactor;
    public bool canTakeDamage = true;
    

    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.timeSinceLevelLoad; // Set the time.
        SoundManager.Instance.PlayEffects("Eruption");
    }
    
    // Update is called once per frame
    void Update()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - initialTime;
        if (timeSinceInitialization >= eruptionTime)
        {
            Destroy(gameObject); //Remove Eruption from game after eruptiontime ends.
        }
       

        if (timeSinceInitialization >= eruptionTime-3)
        {
            if(this.GetComponent<MeshRenderer>() != null)
                this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, opacityFactor/3*(timeSinceInitialization- eruptionTime)); //Make eruption fade out
        }

        Vector2 vec = new Vector2(timeSinceInitialization, timeSinceInitialization);
        transform.localScale = vec * scaleFactor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    private int random; //Define random number.
    private int nextUpdate = 1; //define how often we want to update
    public int inverseProbability;// 1/20 probability for the volcano to erupt every update.
    public GameObject eruption;
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1; //Define time for next update
            random = Random.Range(0, inverseProbability); // Get the random number
            if(random == 0)
            {
                if(!transform.Find("Eruption(Clone)"))
                {
                    GameObject instance = Instantiate(eruption, transform.position, transform.rotation);
                    instance.transform.parent = this.gameObject.transform;
                }
            }
        }
    }
}

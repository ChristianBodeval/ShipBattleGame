using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolMovement : MonoBehaviour
{
    public float changeDirectionTimer;
    public float stepSpeed;

    float targetX;
    float targetY;
    float timerHolder;
    float mapL;
    float mapH;

    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {

        
        mapL = TeleportManager.instance.mapL;
        mapH = TeleportManager.instance.mapH;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time >= timerHolder)
        {
            timerHolder = Time.time + changeDirectionTimer;

            targetX = Random.Range(0, mapL); 
            targetY = Random.Range(0, mapH);
            target = new Vector3(targetX-mapL/2, targetY-mapH/2, 0);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, stepSpeed);
    }
}
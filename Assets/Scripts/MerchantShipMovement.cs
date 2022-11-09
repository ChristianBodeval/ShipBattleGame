using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShipMovement : MonoBehaviour
{
    public float changeDirectionTimer;
    public float movementSpeed;

    float targetX;
    float targetY;
    float timerHolder;
    float mapL;
    float mapH;

    Rigidbody2D rgb;

    public Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        mapL = TeleportManager.instance.mapL;
        mapH = TeleportManager.instance.mapH;
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= timerHolder)
        {
            timerHolder = Time.time + changeDirectionTimer;
            target = GetRandomPointOnScreen();
        }
        
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed);
        //transform.LookAt(target);

        transform.up = target - transform.position;
        //Vector3 direction = transform.position - target;

        //transform.Translate(direction * Time.deltaTime * movementSpeed);



    }



    private Vector3 GetRandomPointOnScreen()
    {
        targetX = Random.Range(0, mapL);
        targetY = Random.Range(0, mapH);
        return target = new Vector3(targetX - mapL / 2, targetY - mapH / 2, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log("COldede");
        timerHolder = Time.time + changeDirectionTimer;
        target = GetRandomPointOnScreen();
    }
    
}

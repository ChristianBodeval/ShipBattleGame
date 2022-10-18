using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    List<GameObject> objectsToTeleport = new List<GameObject>();

    //ArrayList objectsToTeleport = new ArrayList();

    public static TeleportManager instance; //Singleton - used in CanonBall

    float mapH; //Size on Y-axis. Depended on camera size
    float mapL; ////Size on X-axis. Depended on camera size
    Camera camera;
    float halfViewport;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Gets mapL and mapH from the screen size.
        camera = UnityEngine.Camera.main;
        halfViewport = camera.orthographicSize * camera.aspect;

        mapL = halfViewport * 2;
        mapH = camera.orthographicSize * 2;

        //Adds object who should be teleported when hitting border.
        objectsToTeleport.AddRange(GameObject.FindGameObjectsWithTag("Players"));
        Debug.Log("Items in list: " + objectsToTeleport.Capacity);


    }

    public void AddTeleportable(GameObject obj)
    {
        objectsToTeleport.Add(obj);
    }
    public void RemoveTeleportable(GameObject obj)
    {
        objectsToTeleport.Remove(obj); //TODO Can be optimized by instead re-using objects
        
    }


    // Update is called once per frame
    void Update()
    {

        Debug.Log("Items in list: " + objectsToTeleport.Capacity);

        Debug.Log("Items: " + objectsToTeleport.Count);


        //Changes position of the object, if they go out from the screen size. 
        foreach (GameObject obj in objectsToTeleport)
        {
            if (obj != null) {

                if (obj.transform.position.y < 0 - mapH / 2)
                {
                    obj.transform.position += new Vector3(0, +mapH + 1, 0);
                }
                if (obj.transform.position.y > mapH - mapH / 2)
                {
                    obj.transform.position += new Vector3(0, -mapH - 1, 0);
                }
                if (obj.transform.position.x < 0 - mapL / 2)
                {
                    obj.transform.position += new Vector3(+mapL + 1, 0, 0);
                }
                if (obj.transform.position.x > mapL - mapL / 2)
                {
                    obj.transform.position += new Vector3(-mapL - 1, 0, 0);
                }
            }

        }
    }
}

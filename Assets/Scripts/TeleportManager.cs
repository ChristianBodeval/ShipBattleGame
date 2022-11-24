using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    List<GameObject> objectsToTeleport = new List<GameObject>();

    IEnumerable coroutine;

    

    //ArrayList objectsToTeleport = new ArrayList();

    public static TeleportManager instance; //Singleton - used in CanonBall

    public static TeleportManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Gamemanger is null");
            }
            return instance;
        }
    }


    public float mapH; //Size on Y-axis. Depended on camera size
    public float mapL; ////Size on X-axis. Depended on camera size
    Camera camera;
    float halfViewport;

    private void Awake()
    {
        instance = this;
        //Gets mapL and mapH from the screen size.
        camera = UnityEngine.Camera.main;
        halfViewport = camera.orthographicSize * camera.aspect;
        mapL = halfViewport * 2;
        mapH = camera.orthographicSize * 2;
        //Adds object who should be teleported when hitting border.
        objectsToTeleport.AddRange(GameObject.FindGameObjectsWithTag("Players"));
    }

    public void AddTeleportable(GameObject obj)
    {
        objectsToTeleport.Add(obj);
    }
    public void RemoveTeleportable(GameObject obj)
    {
        objectsToTeleport.Remove(obj); //TODO Can be optimized by instead re-using objects
        
    }

    void DisableTrail(GameObject gameObject)
    {
        if (gameObject.GetComponentInChildren<TrailRenderer>())
        {
            Debug.Log("Trail renderer found");
            gameObject.GetComponentInChildren<TrailRenderer>().enabled = false;
        }
    }


    IEnumerator EnableTrail(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        if (gameObject.GetComponentInChildren<TrailRenderer>())
        {

            gameObject.GetComponentInChildren<TrailRenderer>().enabled = true;
        }
    }


   

    // Update is called once per frame
    void Update()
    {


        //Changes position of the object, if they go out from the screen size.
        //TODO This can be optimized
        foreach (GameObject obj in objectsToTeleport)
        {
            if (obj != null) {

                if (obj.transform.position.y < 0 - mapH / 2)
                {
                    DisableTrail(obj);
                    obj.transform.position += new Vector3(0, +mapH + 1, 0);
                    StartCoroutine(EnableTrail(obj));

                }
                if (obj.transform.position.y > mapH - mapH / 2)
                {
                    DisableTrail(obj);
                    obj.transform.position += new Vector3(0, -mapH - 1, 0);
                    StartCoroutine(EnableTrail(obj));
                }
                if (obj.transform.position.x < 0 - mapL / 2)
                {
                    DisableTrail(obj);
                    obj.transform.position += new Vector3(+mapL + 1, 0, 0);
                    StartCoroutine(EnableTrail(obj));
                }
                if (obj.transform.position.x > mapL - mapL / 2)
                {
                    DisableTrail(obj);
                    obj.transform.position += new Vector3(-mapL - 1, 0, 0);
                    StartCoroutine(EnableTrail(obj));
                }
            }

        }
    }
}

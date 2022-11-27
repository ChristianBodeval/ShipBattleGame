using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilePooler : MonoBehaviour
{

    //Pre-spawns gameobjects when the scene is loaded. Does not destoy objects.
    //Reuses gameobjects by disabiling them -> teleporting them to where they should be used -> enabling them again.

    public static ProjectilePooler SharedInstance;
    public List<GameObject> objects;
    public GameObject objectToPool;
    public int amountToPool;


    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize a list of a size containing the specified GameObject
        objects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            objects.Add(tmp);
            tmp.transform.parent = gameObject.transform;
            tmp.SetActive(false);

        }

        foreach (var shot in objects)
        {
            TeleportManager.Instance.AddTeleportable(shot);
        }
    }



    //Finds a object from the list, which is not active. If all pre-defines objects are used - no object is availiable and it will return null.
    public GameObject GetPooledObject()
    {
        foreach (var o in objects)
        {
            if (!o.activeInHierarchy)
                return o;
        }
        return null;
    }
}

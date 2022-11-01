using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilePooler : MonoBehaviour
{
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
        objects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            objects.Add(tmp);
            tmp.SetActive(false);
        }
    }


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

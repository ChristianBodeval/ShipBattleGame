using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class Shooter : MonoBehaviour
{
    // Movement speed in units per second.
    public float speed = 1.0F;

    public GameObject projectilePrefab;



    public void Shoot(Vector3 target)
    {
        
        GameObject canonball = ProjectilePooler.SharedInstance.GetPooledObject();
        if (canonball != null)
        {
            canonball.transform.position = transform.position;
            canonball.transform.rotation = transform.rotation;
            canonball.GetComponent<CanonBall>().Target = target;
            canonball.gameObject.SetActive(true);
        }
        



    }

}



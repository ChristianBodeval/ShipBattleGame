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



    public void Shoot(GameObject shotBy, Vector3 target, float speed, float damage)
    {
        
        GameObject canonball = ProjectilePooler.SharedInstance.GetPooledObject();
        if (canonball != null)
        {
            CanonBall projectileScript = canonball.GetComponent<CanonBall>();
            canonball.transform.position = transform.position;
            canonball.transform.rotation = transform.rotation;
            

            projectileScript.TargetPosition = target;
            projectileScript.Damage = damage;
            projectileScript.ProjectileSpeed = speed;
            projectileScript.ShotBy = shotBy;
            
            canonball.gameObject.SetActive(true);
        }
        



    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class Shooter : MonoBehaviour
{
    // Movement speed in units per second.
    public float speed = 1.0F;

    public bool fireOnBullets;

    public GameObject projectilePrefab;

    public ParticleSystem smoke;
    public ParticleSystem mussleflash;

    public Vector3 currentPosition;

    public void Start()
    {
        
    }


    public void Shoot(GameObject shotBy, Vector3 target, float speed, float damage)
    {
        GameObject canonball = ProjectilePooler.SharedInstance.GetPooledObject();
        mussleflash.Play();
        smoke.gameObject.transform.parent = gameObject.transform;
        smoke.gameObject.transform.position = gameObject.transform.position;
        smoke.Play();
        smoke.gameObject.transform.parent = null;
        if (canonball != null)
        {
            CanonBall projectileScript = canonball.GetComponent<CanonBall>();
            canonball.transform.position = transform.position;
            canonball.transform.rotation = transform.rotation;

            projectileScript.TargetPosition = target;
            projectileScript.Damage = damage;
            projectileScript.ProjectileSpeed = speed;
            projectileScript.ShotBy = shotBy;
            
            projectileScript.fireOnBullet = fireOnBullets;
            canonball.gameObject.SetActive(true);
            TeleportManager.Instance.AddTeleportable(canonball);
        }
    }

}



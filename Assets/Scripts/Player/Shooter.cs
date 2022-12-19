using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Movement speed in units per second.
    public float speed = 1.0F;
    public bool fireOnBullets;
    public GameObject projectilePrefab;
    public ParticleSystem smoke;
    public ParticleSystem mussleflash;
    public Vector3 currentPosition;

    public void Shoot(GameObject shotBy, Vector3 target, float speed, float damage)
    {
        //Gets cannonball object
        GameObject cannonBall = ProjectilePooler.SharedInstance.GetPooledObject();
        //Handling particles
        mussleflash.Play();
        smoke.gameObject.transform.parent = gameObject.transform;
        smoke.gameObject.transform.position = gameObject.transform.position;
        smoke.Play();
        smoke.gameObject.transform.parent = null;

        //Assigns values and position of cannonball
        if (cannonBall != null)
        {
            CannonBall projectileScript = cannonBall.GetComponent<CannonBall>();
            cannonBall.transform.position = transform.position;
            cannonBall.transform.rotation = transform.rotation;

            projectileScript.TargetPosition = target;
            projectileScript.Damage = damage;
            projectileScript.ProjectileSpeed = speed;
            projectileScript.ShotBy = shotBy;
            
            projectileScript.fireOnBullet = fireOnBullets;
            cannonBall.gameObject.SetActive(true);
            TeleportManager.Instance.AddTeleportable(cannonBall);
        }
    }

}



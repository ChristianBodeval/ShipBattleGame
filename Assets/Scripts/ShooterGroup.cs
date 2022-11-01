using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGroup : MonoBehaviour
{
    GameObject[] shooterGameObjects;
    public GameObject shooterPrefab;
    public Vector2[] endPoints;
    Vector2[] startPoints;

    [Range(1, 20)]
    public float range;

    [Range(2, 10)]
    public int corners;

    [Range(1, 10)]
    public float shipSize;

    [Range(0, 180)]
    public float maxAngle;


    void Awake()
    {
        corners = 10;
        endPoints = new Vector2[corners + 2];
        startPoints = new Vector2[corners + 2];
        shooterGameObjects = new GameObject[corners];
    }

    void Update()
    {
        SetLinePoints(startPoints, endPoints);
        SpawnShooters(startPoints, endPoints);
        DestroyUnusedShooters();

        if (Input.anyKeyDown)
        {
            Fire();
        }
    }


    void SetLinePoints(Vector2[] startPoints, Vector2[] endPoints)
    {
        //Step values
        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners - 1);
        //Current values
        float currentPointDistance = 0 - (shipSize / 2);
        float currentAngle = 0 - (maxAngle / 2) + transform.rotation.eulerAngles.z;

        for (int i = 0; i <= corners; i++)
        {
            Vector3 start = transform.up * currentPointDistance; 
            Vector3 direction = GetVectorFromAngle(currentAngle);
            Vector3 target = direction * range + start;

            startPoints[i] = start;
            endPoints[i] = target;

            currentPointDistance += pointDistanceStep;
            currentAngle += angleStep;
        }
    }

    //Draws lines between startPoints and endPoints
    void OnDrawGizmosSelected()
    {

        if (startPoints != null && endPoints != null)
        {
            if (startPoints.Length > 0 && endPoints.Length > 0)
            {
                for (int i = 0; i < corners; i++)
                {

                    Gizmos.DrawLine((Vector3)startPoints[i] + transform.position, (Vector3)endPoints[i] + transform.position);
                }
            }
        }

    }
    
    public void Fire()
    {
        for (int i = 0; i < shooterGameObjects.Length; i++)
        {
            Shooter shooterScript = shooterGameObjects[i].GetComponent<Shooter>();
            Vector3 position = shooterGameObjects[i].transform.position;

            Vector3 direction = endPoints[i] - startPoints[i];

            shooterScript.Shoot(direction + shooterGameObjects[i].transform.position);
        }
        
    }    

    //Spawn canons at startPoints and make them point towards endPoints.
    public void SpawnShooters(Vector2[] startPoints, Vector2[] endPoints)
    {
        for (int i = 0; i < corners; i++)
        {
            float angle = GetAngleFromVector(endPoints[i] - startPoints[i]);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            if (shooterGameObjects[i] != null)
            {
                shooterGameObjects[i].transform.position = (Vector3)startPoints[i] + GetComponentInParent<Transform>().position;
                shooterGameObjects[i].transform.rotation = rotation;
                continue;
            }


            GameObject canonClone;
            canonClone = Instantiate(shooterPrefab, (Vector3)startPoints[i] + transform.position, rotation);

            shooterGameObjects[i] = canonClone;
        }

    }

    void DestroyUnusedShooters()
    {
        for (int i = 0; i < shooterGameObjects.Length; i++)
        {
            if (i >= corners)
            {
                GameObject.Destroy(shooterGameObjects[i]);
            }
        }
    }

    public static float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        float angle = n;

        return angle;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    


    
}

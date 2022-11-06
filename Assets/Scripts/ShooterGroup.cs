using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterGroup : MonoBehaviour
{
    GameObject[] canonGameObjects;
    public GameObject shooterPrefab;
    public Vector2[] endPoints;
    Vector2[] startPoints;

    [Range(1, 20)]
    public float range;

    [Range(1, 10)]
    public int corners;

    [Range(1, 10)]
    public float shipSize;

    [Range(0, 180)]
    public float maxAngle;

    [Range(-2, 0)]
    public float startCurve;

    public bool isDrawingLines = true;

    void Awake()
    {
        corners = 10; //Initialize at max
        endPoints = new Vector2[corners + 2];
        startPoints = new Vector2[corners + 2];
        canonGameObjects = new GameObject[corners];
    }

    void Update()
    {
        SetLinePoints(startPoints, endPoints);
        SpawnShooters(startPoints, endPoints);
        DestroyUnusedShooters();

        if (Input.anyKeyDown)
        {

            //Tester - Should be called by the shooting
            Fire();
        }
    }

    //Sets points for the lines, which the shots will follow
    // Makes x amount of points in a cone from the 
    void SetLinePoints(Vector2[] startPoints, Vector2[] endPoints)
    {
        //For one corner, just update
        //Step values
        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners - 1);
        //Current values
        float currentPointDistance = 0 - (shipSize / 2);
        float currentAngle = 0 - (maxAngle / 2) + transform.rotation.eulerAngles.z;

        if(corners == 1)
        {
            currentPointDistance = 0;
            currentAngle = 0;
        }
        
        for (int i = 0; i <= corners; i++)
        {

            Vector3 start = transform.up * currentPointDistance;                                //Gets start position by moving the point a little up or down dependenet on currentPointDistance
            start += startCurve * Mathf.Abs(currentPointDistance) * transform.right;            //Curves the points, by moving them a little left or nothing at all. 
            Vector3 direction = GetVectorFromAngle(currentAngle);                               //Vector from the angle
            Vector3 target = direction * range + start;                                         //Ending of the line

            startPoints[i] = start;
            endPoints[i] = target;

            currentPointDistance += pointDistanceStep;
            currentAngle += angleStep;
        }

        

    }

    //Draws lines between startPoints and endPoints in Gizmo
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
    //Fires the canons by iteration through all canons and shooting them at the target. 
    public void Fire()
    {
        for (int i = 0; i < canonGameObjects.Length; i++)
        {
            Shooter shooterScript = canonGameObjects[i].GetComponent<Shooter>();
            Vector3 position = canonGameObjects[i].transform.position;

            Vector3 direction = endPoints[i] - startPoints[i]; //TODO Kan dette skrives kun med endPoints?

            shooterScript.Shoot(direction + canonGameObjects[i].transform.position);
        }
        
    }    

    //Spawn canons at startPoints and make them point towards endPoints.
    public void SpawnShooters(Vector2[] startPoints, Vector2[] endPoints)
    {
        for (int i = 0; i < corners; i++)
        {
            float angle = GetAngleFromVector(endPoints[i] - startPoints[i]);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            if (canonGameObjects[i] != null)
            {
                canonGameObjects[i].transform.position = (Vector3)startPoints[i] + GetComponentInParent<Transform>().position;
                canonGameObjects[i].transform.rotation = rotation;
                continue;
            }


            GameObject canonClone;
            canonClone = Instantiate(shooterPrefab, (Vector3)startPoints[i] + transform.position, rotation);

            canonGameObjects[i] = canonClone;
        }

    }

    //Check for the current size of corner, if corners is reduced in the inspecter, canons will still be left. Delete them. 
    void DestroyUnusedShooters()
    {
        for (int i = 0; i < canonGameObjects.Length; i++)
        {
            if (i >= corners)
            {
                GameObject.Destroy(canonGameObjects[i]);
            }
        }
    }
    //Helper function to get an angle from any vector
    public static float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        float angle = n;

        return angle;
    }
    //Helper function to get a vector from any angle. -> Angle is between 0 and 360
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}

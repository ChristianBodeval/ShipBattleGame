using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.FilePathAttribute;

public class ShooterGroup : MonoBehaviour
{
    public bool renderLines;
    private float projectileSpeed;
    public GameObject linePrefab;

    public float shootersTotalDamage;
    public GameObject[] canonGameObjects;
    public GameObject shooterPrefab;
    public Vector2[] endPoints;
    Vector2[] startPoints;
    public GameObject[] lines;

    private float range;
    private int shooters;
    private float shipSize;
    private float maxAngle;
    private float startCurve;
    private bool fireOnBullets;

    public bool isDrawingLines = true;
    public float Range { get => range; set => range = value; }
    public int Shooters { get => shooters; set => shooters = value; }
    public float ShipSize { get => shipSize; set => shipSize = value; }
    public float MaxAngle { get => maxAngle; set => maxAngle = value; }
    public float StartCurve { get => startCurve; set => startCurve = value; }
    public bool FireOnBullets { get => fireOnBullets; set => fireOnBullets = value; }

    void Awake()
    {
        shooters = 10; //Initialize at max
        endPoints = new Vector2[shooters + 2];
        startPoints = new Vector2[shooters + 2];
        canonGameObjects = new GameObject[shooters];
        lines = new GameObject[shooters + 2];

        fireOnBullets = false;
    }

    void FixedUpdate()
    {
        SetLinePoints(startPoints, endPoints);
        SpawnShooters(startPoints, endPoints);
        DestroyUnusedShooters();
    }

    public void SetLinesColor(Color color)
    {
        foreach (GameObject canon in canonGameObjects)
        {
            if (canon == null)
            {
                continue;
            }
            if (canon.GetComponent<LineRenderer>().material != null)
            {
                canon.GetComponent<LineRenderer>().startColor = color;
                canon.GetComponent<LineRenderer>().endColor = color;
            }
        }
    }

    //Sets points for the lines, which the shots will follow
    // Makes x amount of points in a cone from the 
    void SetLinePoints(Vector2[] startPoints, Vector2[] endPoints)
    {
        //For one corner, just update
        //Step values
        float angleStep = maxAngle / (shooters - 1);
        float pointDistanceStep = shipSize / (shooters - 1);
        //Current values
        float currentPointDistance = 0 - (shipSize / 2);
        float currentAngle = 0 - (maxAngle / 2) + transform.rotation.eulerAngles.z;

        if (shooters == 1)
        {
            currentPointDistance = 1;
            currentAngle = 0.00001f;

            Vector3 start = Vector3.zero;                                
            Vector3 direction = transform.right;                              
            Vector3 target = direction * range + start;                                         

            startPoints[0] = start;
            endPoints[0] = target;
        }

        else
        {
            for (int i = 0; i <= shooters; i++)
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
    }

    //Draws lines between startPoints and endPoints in Gizmo
    void OnDrawGizmosSelected()
    {
        if (startPoints != null && endPoints != null)
        {
            if (startPoints.Length > 0 && endPoints.Length > 0)
            {

                for (int i = 0; i < shooters; i++)
                {
                    Gizmos.DrawLine((Vector3)startPoints[i] + transform.position, (Vector3)endPoints[i] + transform.position);
                }
            }
        }
    }


    //Fires the canons by iteration through all canons and shooting them at the target. 
    public void Fire(float projectileSpeed, float totalDamage)
    {
        for (int i = 0; i < shooters; i++)
        {
            Shooter shooterScript = canonGameObjects[i].GetComponent<Shooter>();
            shooterScript.fireOnBullets = fireOnBullets;

            Vector3 position = canonGameObjects[i].transform.position;
            Vector3 direction = endPoints[i] - startPoints[i] + (Vector2)canonGameObjects[i].transform.position; //TODO Kan dette skrives kun med endPoints?
            GameObject shotBy = transform.parent.gameObject;

            shooterScript.Shoot(shotBy, direction, projectileSpeed, totalDamage / shooters);
        }
    }

    //Spawn canons at startPoints and make them point towards endPoints.
    public void SpawnShooters(Vector2[] startPoints, Vector2[] endPoints)
    {
        for (int i = 0; i < shooters; i++)
        {
            float angle = GetAngleFromVector(endPoints[i] - startPoints[i]);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            if (canonGameObjects[i] != null)
            {
                canonGameObjects[i].transform.position = (Vector3)startPoints[i] + GetComponentInParent<Transform>().position;
                canonGameObjects[i].transform.rotation = rotation;

                if (renderLines)
                {
                    if (canonGameObjects[i].GetComponent<LineRenderer>() != null)
                    {
                        canonGameObjects[i].GetComponent<LineRenderer>().enabled = true;
                        canonGameObjects[i].GetComponent<LineRenderer>().SetPosition(0, canonGameObjects[i].transform.position);
                        canonGameObjects[i].GetComponent<LineRenderer>().SetPosition(1, (Vector3)endPoints[i] - (Vector3)startPoints[i] + canonGameObjects[i].transform.position);
                    }

                }
                else
                {
                    canonGameObjects[i].GetComponent<LineRenderer>().enabled = false;
                }

                continue;
            }


            GameObject cannon;
            cannon = Instantiate(shooterPrefab, (Vector3)startPoints[i] + transform.position, rotation);

            if (shooters == 1)
            {
                cannon.transform.localScale *= 3;
            }

            cannon.transform.parent = gameObject.transform;
            canonGameObjects[i] = cannon;
        }

    }


    //Check for the current size of corner, if corners is reduced in the inspecter, canons will still be left. Delete them. 
    void DestroyUnusedShooters()
    {
        for (int i = 0; i < canonGameObjects.Length; i++)
        {
            if (i >= shooters)
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

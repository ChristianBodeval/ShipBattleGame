using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using System;

public class ConeCollider : MonoBehaviour
{
    public bool inverted = false;

    GameObject[] canons;


    public GameObject canonPrefab;

    Vector2[] endPoints;
    Vector2[] startPoints;

    [Range(1,20)]
    public float range;

    [Range(2,10)]
    public int corners;

    [Range(1,10)]
    public float shipSize;

    [Range(10,180)]
    public float maxAngle;

    private float currentAngle;
    private float currentShipSizeStep;
    private PolygonCollider2D pc;
    // Start is called before the first frame update
    void Awake()
    {
        corners = 10;
        pc = GetComponent<PolygonCollider2D>();
        endPoints = new Vector2[corners+2];
        startPoints = new Vector2[corners+2];
        canons = new GameObject[corners];


    }


    void GetPoints()
    {
        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners-1);

        currentShipSizeStep = 0 - (shipSize / 2);

        Quaternion parentRotation = GetComponentInParent<Transform>().rotation;
        Vector3 parentRotationVector = new Vector3(parentRotation.x, parentRotation.y, parentRotation.z);


        currentAngle = 0 - (maxAngle / 2) + Vector3.Angle(parentRotationVector, Vector3.down);

        for (int i = 0; i <= corners; i++)
        {
            
            Vector3 start =  transform.up * currentShipSizeStep;

            

            Vector3 direction = GetVectorFromAngle(currentAngle);

            Vector3 target = direction * range + start;

            endPoints[i] = (Vector2) target;
            startPoints[i] = (Vector2) start;

            currentShipSizeStep += pointDistanceStep;
            currentAngle += angleStep;


        }

        endPoints[corners] = new Vector2(0, (shipSize / 2));
        endPoints[corners + 1] = new Vector2(0, -(shipSize / 2));


    }

    //Draws lines between startPoints and endPoints
    void OnDrawGizmosSelected()
    {

        if(startPoints != null && endPoints != null)
        {
            if (startPoints.Length > 0 && endPoints.Length > 0)
            {
                for (int i = 0; i < corners; i++)
                {
                    Debug.Log("Drawing");
                    Gizmos.DrawLine((Vector3)startPoints[i] + transform.position, (Vector3)endPoints[i] + transform.position);
                }
            }
        }

    }

    //Spawn canons at startPoints and make them point towards endPoints.
    public void SpawnCanons()
    {
        for (int i = 0; i < corners; i++)
        {
            float angle = GetAngleFromVector((Vector3)endPoints[i]);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            if (canons[i] != null)
            {
                canons[i].transform.position = (Vector3)startPoints[i] + GetComponentInParent<Transform>().position;
                canons[i].transform.rotation = rotation;
                continue;
            }


            GameObject canonClone;
            canonClone = Instantiate(canonPrefab, (Vector3)startPoints[i] + transform.position, rotation);

            canons[i] = canonClone;

        }
            
    }

    public static int GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        int angle = Mathf.RoundToInt(n);

        return angle;
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    //Inverts y-values of 
    void Invert()
    {
        if(inverted)
        {
            for (int i = 0; i < endPoints.Length; i++)
            {
                
                endPoints[i] = new Vector2(endPoints[i].x * -1, endPoints[i].y);                                        //Flips x-values of endpoints
                //pc.points[i] = new Vector2(pc.points[i].x * -1, pc.points[i].y);
            }
        }
    }

    void DestroyUnusedCannons()
    {
        for (int i = 0; i < canons.Length; i++)
        {
            if (i >= corners)
            {
                GameObject.Destroy(canons[i]);
            }
        }
    }

    void Update()
    {

        GetPoints();
        Invert();
        SpawnCanons();

        pc.points = new Vector2[endPoints.Length];
        pc.points = endPoints;
    }
}

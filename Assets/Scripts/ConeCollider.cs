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
    GameObject[] canons;

    public GameObject canonPrefab;

    Vector2[] endPoints;
    Vector2[] relativeEndpoints;
    Vector2[] startPoints;

    public float testAngle = 0;

    [Range(1,20)]
    public float range;

    public int maxCornerRange;
    [Range(2,10)]
    public int corners;

    [Range(1,10)]
    public float shipSize;

    [Range(0,180)]
    public float maxAngle;

    private PolygonCollider2D pc;

    void Awake()
    {
        pc = GetComponent<PolygonCollider2D>();
        endPoints = new Vector2[corners+2];
        relativeEndpoints = new Vector2[corners+2];
        startPoints = new Vector2[corners+2];
        canons = new GameObject[corners];
    }

    void SetLinePoints(Vector2[] startPoints, Vector2[] endPoints)
    {
        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners-1);
        float playerRotationInAngles = transform.rotation.eulerAngles.z; //The gameobjects rotation in world space
        float currentPointDistance = 0 - (shipSize / 2);
        float currentAngle = 0 - (maxAngle / 2) + playerRotationInAngles;

        for (int i = 0; i <= corners; i++)
        {
            Vector3 start =  transform.up * currentPointDistance;
            Vector3 direction = GetVectorFromAngle(currentAngle);
            Vector3 target = direction * range + start;

            startPoints[i] = (Vector2) start;
            endPoints[i] = (Vector2)target;

            currentPointDistance += pointDistanceStep;
            currentAngle += angleStep;
        }
    }

    void SetLinePoints2(Vector2[] startPoints, Vector2[] endPoints)
    {
        //Step values
        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners - 1);
        //Current values
        float currentPointDistance = 0 - (shipSize / 2);
        float currentAngle = 0 - (maxAngle / 2) + transform.rotation.eulerAngles.z;

        for (int i = 0; i <= corners; i++)
        {
            Vector2 start = transform.up * currentPointDistance;
            Vector2 direction = GetVectorFromAngle(currentAngle);
            Vector2 target = direction * range + start;

            startPoints[i] = start;
            endPoints[i] = target;

            currentPointDistance += pointDistanceStep;
            currentAngle += angleStep;
        }
    }


    Vector2[] GetColliderPoints(int corners)
    {
        Vector2[] returnArray = new Vector2[corners+2];

        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners - 1);
        float playerRotationInAngles = transform.rotation.eulerAngles.z; //The gameobjects rotation in world

        float currentShipSizeStep = 0 - (shipSize / 2);
        float currentAngle = 0 - (maxAngle / 2);

        for (int i = 0; i <= corners; i++)
        {
            Vector3 start = Vector3.up * currentShipSizeStep;
            Vector3 direction = GetVectorFromAngle(currentAngle);
            Vector3 target = direction * range + start;

            returnArray[i] = (Vector2)target;

            currentShipSizeStep += pointDistanceStep;
            currentAngle += angleStep;
        }

        returnArray[corners] = new Vector2(0, (shipSize / 2));
        returnArray[corners + 1] = new Vector2(0, -(shipSize / 2));

        return returnArray;
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
            float angle = GetAngleFromVector(endPoints[i] - startPoints[i]);
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
        SetLinePoints(startPoints, endPoints);
        SpawnCanons();
        DestroyUnusedCannons();
        pc.points = GetColliderPoints(corners);
    }
}

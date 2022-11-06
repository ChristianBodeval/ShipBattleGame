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

    bool running = false;

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
        pc = GetComponent<PolygonCollider2D>();
        endPoints = new Vector2[corners+2];
        startPoints = new Vector2[corners+2];

        corners = 10;

    }



    void GetPoints()
    {

        float angleStep = maxAngle / (corners - 1);
        float pointDistanceStep = shipSize / (corners-1);

        currentShipSizeStep = 0 - (shipSize / 2);

        currentAngle = 0 - (maxAngle / 2);

        for (int i = 0; i <= corners; i++)
        {
            
            Vector3 start = new Vector3(0, currentShipSizeStep, 0);

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

    void OnDrawGizmosSelected()
    {


        if (startPoints.Length > 0 && endPoints.Length > 0)
        {
            for (int i = 0; i < corners; i++)
            {
                Debug.Log("Drawing");
                Gizmos.DrawLine(startPoints[i], endPoints[i]);
            }
        }

    }


    public static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    void Update()
    {
        GetPoints();

        pc.points = new Vector2[endPoints.Length];
        Debug.Log("Is it" + pc.points.IsFixedSize);
        pc.points = endPoints;
    }
}

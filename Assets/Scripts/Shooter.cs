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

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public Vector3 endMarker;

    private Vector3 target;

    public void Shoot(Vector3 target)
    {
        projectilePrefab = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectilePrefab.GetComponent<CanonBall>().Target = target;
    }

}

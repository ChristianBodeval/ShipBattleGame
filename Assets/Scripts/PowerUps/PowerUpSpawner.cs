using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpSpawner : MonoBehaviour
{
    public int rando;
    public enum Type {Health , Attack , Speed, NumberOfTypes };

    public GameObject powerUpH;
    public GameObject powerUpA;
    public GameObject powerUpS;

    void SpawnRandom()
    {
        Type rando = (Type)Random.Range(0, (int)Type.NumberOfTypes);
        

        switch (rando)
        {
            case Type.Health:
                Instantiate(powerUpH, transform.position, transform.rotation);
                break;
            case Type.Attack:
                Instantiate(powerUpA, transform.position, transform.rotation);
                break;
            case Type.Speed:
                Instantiate(powerUpS, transform.position, transform.rotation);
                break;
        }
    }

    private void Start()
    {
        SpawnRandom();
    }
}
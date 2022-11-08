using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpSpawner : MonoBehaviour
{
    public Type random;
    public enum Type {Health , Attack , Speed, NumberOfTypes };

    public GameObject powerUpH;
    public GameObject powerUpA;
    public GameObject powerUpS;

    void SpawnRandom()
    {

        random = (Type)Random.Range(0, (int)Type.NumberOfTypes);

        GameObject randomPowerUp;

        switch (random)
        {
            case Type.Health:
                randomPowerUp = Instantiate(powerUpH, transform.position, transform.rotation);
                break;
            case Type.Attack:
                randomPowerUp = Instantiate(powerUpA, transform.position, transform.rotation);
                break;
            case Type.Speed:
                randomPowerUp = Instantiate(powerUpS, transform.position, transform.rotation);
                break;  
        }
    }

    

    private void Start()
    {

        SpawnRandom();
    }

    
}
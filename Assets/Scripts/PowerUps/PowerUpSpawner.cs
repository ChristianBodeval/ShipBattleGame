using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private Type randomPowerup;
    public enum Type {Health , Attack , Speed, NumberOfTypes };

    public GameObject powerUpH;
    public GameObject powerUpA;
    public GameObject powerUpS;


    private void Start()
    {
        SpawnRandom();
    }

    void SpawnRandom()
    {
        //1 to 4 to Ignore health
        randomPowerup = (Type)Random.Range(1, (int)Type.NumberOfTypes);

        GameObject randomPowerUp;

        switch (randomPowerup)
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

    

    

    
}
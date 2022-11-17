
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    public Color player1Color = Color.red;
    public Color player2Color = Color.blue;
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    public bool spawnPlayer1;
    public bool spawnPlayer2;
    
    private static GameManager instance; //Singleton

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Gamemanger is null");
            }
            return instance;
        }
    }

    public Transform spawn1;
    public Transform spawn2;


    public List<GameObject> players = new List<GameObject>();


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

        if (spawnPlayer1)
        {
            var p1 = PlayerInput.Instantiate(player1Prefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
            players.Add(p1.gameObject);
            p1.transform.position = spawn1.transform.position;
            p1.transform.rotation = spawn1.transform.rotation;
            TeleportManager.instance.AddTeleportable(p1.gameObject);
        }


        if (spawnPlayer2)
        {
            var p2 = PlayerInput.Instantiate(player2Prefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);

            //Set the two players
            players.Add(p2.gameObject);

            //Set position to spawnpoints
            p2.transform.position = spawn2.transform.position;
            p2.transform.rotation = spawn2.transform.rotation;

            //Make them teleportable
            TeleportManager.instance.AddTeleportable(p2.gameObject);
        }
    }
}

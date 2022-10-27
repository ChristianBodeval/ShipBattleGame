
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Color player1Color = Color.red;
    public Color player2Color = Color.blue;
    public GameObject playerPrefab;
    

    public static GameManager instance; //Singleton

    public ShipManager player1ShipManager;
    public ShipManager player2ShipManager;

    public Transform spawn1;
    public Transform spawn2;


    public List<GameObject> players = new List<GameObject>();

  //  private void Start()
    //{
        
    //}

    // Start is called before the first frame update
   /* void Awake()
    {

        var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);

        //Set the two players
        players.Add(p1.gameObject);
        players.Add(p2.gameObject);

        //Set position to spawnpoints
        p1.transform.position = spawn1.transform.position;
        p1.transform.rotation = spawn1.transform.rotation;
        p2.transform.position = spawn2.transform.position;
        p2.transform.rotation = spawn2.transform.rotation;

        //Set colors of the players
        p1.GetComponent<SpriteRenderer>().color = player1Color;
        p2.GetComponent<SpriteRenderer>().color = player2Color;



        //Make them teleportable
        TeleportManager.instance.AddTeleportable(p1.gameObject);
        TeleportManager.instance.AddTeleportable(p2.gameObject);
    }
*/
}
  

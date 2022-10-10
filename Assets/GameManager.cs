
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Color player1Color = Color.red;
    public Color player2Color = Color.blue;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {

        var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
        p1.GetComponent<SpriteRenderer>().color = player1Color;
        p2.GetComponent<SpriteRenderer>().color = player2Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

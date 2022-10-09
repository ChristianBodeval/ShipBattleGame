using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardLeft", pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "KeyboardRight", pairWithDevice: Keyboard.current);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

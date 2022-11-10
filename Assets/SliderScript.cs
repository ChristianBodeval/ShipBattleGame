using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    GameObject ship;
    public Slider slider;


    GameManager gameManager;

    public int playerNumber;
    
    // Start is called before the first frame update
    void Start()
    {

        ship = GameManager.Instance.players[playerNumber];
        Debug.Log(ship.GetComponent<ShipManager>().StartingHealth);
        slider.maxValue = ship.GetComponent<ShipManager>().StartingHealth;
        

    }

    // Update is called once per frame
    void Update()
    {
        gameManager = GameManager.Instance;
        Debug.Log(gameManager);

        slider.value = ship.GetComponent<ShipManager>().CurrentHealth;
    }
}

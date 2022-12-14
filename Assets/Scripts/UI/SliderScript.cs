using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    GameObject ship;
    public Slider slider;
    private float red = 0;
    private float green = 255;
    private float blue = 0;
    private Color color = new Color(0 / 255f, 255 / 255f, 0 / 255f);
    private Color normalColor;

    private int oldHealthUpdateCounter = 0;
    public int oldHealthUpdateRate = 10;
    private float oldHealth;
    GameManager gameManager;

    private float tempTime;

    private bool blinkingEvent = false;
    private int blinkCounter = 0;
    public int numberOfBlinks = 2;

    public int playerNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(red / 255f, green / 255f, blue / 255f);
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
        ship = GameManager.Instance.players[playerNumber].gameObject;
        slider.maxValue = ship.GetComponent<ShipManager>().StartingHealth;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameManager = GameManager.Instance;
        
        slider.value = ship.GetComponent<ShipManager>().CurrentHealth;


        //if (slider.value < slider.maxValue/2) green = (255/(slider.maxValue/2))*(slider.value -(slider.maxValue/2));
        
        if (slider.value > slider.maxValue / 2)
        {
            green = 255;
            red = 255 - ((255 / (slider.maxValue / 2)) * (slider.value - slider.maxValue/2));
        }
        else if (slider.value < slider.maxValue / 2)
        {
            green = ((255 / (slider.maxValue / 2)) * (slider.value - slider.maxValue / 2));
            red = 255;
        }

        //Debug.Log("red---: " + red);
        //Debug.Log("green-: " + green);
        color = new Color(red / 255f, green / 255f, blue / 255f);
        
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }
}

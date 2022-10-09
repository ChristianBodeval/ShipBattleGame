using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float turnValue;
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    //Rotation values
    public float turnValueHolder;
    public float maxAcceleration = 5;
    public float startTurnAcceleration = 0.1F;
    public float currentTurnAcceleration;
    public float turnAccelerationSpeed;


    //Movement values
    public int gear = 0;
    public int maxGears = 2;
    public float speed = 5f;
    //For Turn 3

    public GameObject playerPrefab;

    public float interpolationNumber = 1f;


    public float movementFactor;
    PlayerInputActions playerInputActions;

    Vector3 rotationDirection;


   

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
    }



    //Sets a gear between 0 and maxGears. W for going up a gear, S for going down.  
    public void AccelerateOnStarted(InputAction.CallbackContext context)
    {


        Debug.Log(this.name + " is calling");
        float value = context.ReadValue<float>();

        if (gear < maxGears-1)
        {
            if (value > 0)
                gear++;
        }
        if (gear > 0)
        {
            if (value < 0)
                gear--;
        }
        /*
        Debug.Log("W or D: " + value);
        Debug.Log("Context: " + context);
        Debug.Log("Gear: " + gear);
        */

    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        turnValue = context.ReadValue<float>();
    }


    

    /*
     * Another way to move forward, where you can use the weight of the rigidbody
     * Good for drifting
    public void forwardSpeed2()
    {
        rb.AddForce(transform.up * speed * gear, ForceMode2D.Force);
    }
    */

    public void forwardSpeed()
    {
        movementFactor = Mathf.Lerp(movementFactor, gear, Time.deltaTime);
        transform.Translate(0f, movementFactor*speed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        calculateAcceleration();
        forwardSpeed();
    }


    /// <summary>
    /// Calculates increases rotationAcceleration when turning and deaccelerates when not turning
    /// </summary>
    void calculateAcceleration()
    {
        if (turnValueHolder != 0)
        {
            if (currentTurnAcceleration < maxAcceleration)
                currentTurnAcceleration += turnAccelerationSpeed * Time.deltaTime;
        }
        else if (currentTurnAcceleration > 0)
        {
            currentTurnAcceleration -= turnAccelerationSpeed * Time.deltaTime;
        }
        else if (currentTurnAcceleration <= 0)
        {
            currentTurnAcceleration = 0;
        }

    }

    

    void Turn()
    {
        turnValueHolder = Mathf.Lerp(turnValueHolder, turnValue, interpolationNumber);
        //turnInputValue = Mathf.SmoothStep(turnInputValue,playerInputActions.Player.Turn.ReadValue<float>(),5f);

        if (turnValueHolder != 0)
            rotationDirection = new Vector3(0f, 0f, turnValueHolder);

        transform.Rotate(rotationDirection * Time.deltaTime * currentTurnAcceleration);
    }



}

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject playerPrefab;
    public ShipManager shipManager;

    //All the following values are public, so i makes it easier to test in the inspector
    //Values which have an impact on the movement
    private float maxTurnSpeed; // 1.5
    private float turnAcceleration; // 0.8
    private float smoothTurningFactor; //0.1
    private float smoothMovementFactor; //0.1
    private int numberOfGears; // 2
    private float maxMovementSpeed; //0.11

    //Holder values
    private float turnInputValue;
    private Vector3 turnDirection;
    private float currentTurnAcceleration;
    private float currentTurnValue;
    private float currentMoveValue;
    private int currentGear; //The current gear

    //Accessor values
    public float MaxTurnSpeed { get => maxTurnSpeed; set => maxTurnSpeed = value; }
    public float TurnAcceleration { get => turnAcceleration; set => turnAcceleration = value; }
    public float SmoothTurningFactor { get => smoothTurningFactor; set => smoothTurningFactor = value; }
    public float SmoothMovementFactor { get => smoothMovementFactor; set => smoothMovementFactor = value; }
    public int NumberOfGears { get => numberOfGears; set => numberOfGears = value; }
    public float MaxMovementSpeed { get => maxMovementSpeed; set => maxMovementSpeed = value; }

    

    private void Start()
    {
        shipManager = GetComponent<ShipManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateTurnAcceleration();
        Turn();
        MoveForward();
    }

    //Sets a gear between 0 and maxGears. W for going up a gear, S for going down.  
    public void OnMove(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        //Increases or decreases gear
        if (currentGear < numberOfGears-1)
        {
            if (value > 0)
                currentGear++;
        }
        if (currentGear > 0)
        {
            if (value < 0)
                currentGear--;
        }
    }



    public void MoveForward()
    {
        //Smoothly change the turnValue to the inputValue
        currentMoveValue = Mathf.Lerp(currentMoveValue, currentGear, smoothMovementFactor);
        transform.Translate(0f, currentMoveValue * maxMovementSpeed, 0f);

    }

    //Sets input value
    public void OnTurn(InputAction.CallbackContext context)
    {
        turnInputValue = context.ReadValue<float>();
    }

    /// <summary>
    /// Handles the acceleration of turning.
    /// </summary>
    void CalculateTurnAcceleration()
    {
        //Is player turning, then accelerate turning, since input value is either 1 or -1
        if (currentTurnValue != 0)
        {
            if (currentTurnAcceleration < maxTurnSpeed)
                currentTurnAcceleration += turnAcceleration;
        }
        // If not turning, then deaccelerate turning
        else if (currentTurnAcceleration > 0)
        {
            currentTurnAcceleration -= turnAcceleration;
        }
        //When acceleration goes negative, set i to 0
        else if (currentTurnAcceleration <= 0)
        {
            currentTurnAcceleration = 0;
        }

    }

    //Turns the ship smoothly
    void Turn()
    {
        //Smoothly change the turnValue to the inputValue
        currentTurnValue = Mathf.Lerp(currentTurnValue, turnInputValue, smoothTurningFactor);

        //Update turnDirection only when turning
        if (currentTurnValue != 0)
            turnDirection = new Vector3(0f, 0f, currentTurnValue);

        //Turn
        transform.Rotate(turnDirection * currentTurnAcceleration);
    }

}

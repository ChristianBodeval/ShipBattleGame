using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float turnInputValue;
    private Vector3 turnDirection;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    public GameObject playerPrefab;
    private PlayerInputActions playerInputActions;

    //All the following values are public, so i makes it easier to test in the inspector
    //Turn values
    public float turnValue;
    public float maxAcceleration = 5f; 
    public float turnAcceleration;
    public float turnAccelerationSpeed;
    public float smoothTurningFactor = 0.1f;

    //Accelerate values
    public float moveValue;
    public int gear = 0; //The current gear
    public int maxGears = 2;
    public float speed = 5f;
    public float smoothMovementFactor = 1;

   
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
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
    }



    public void MoveForward()
    {
        //Smoothly change the turnValue to the inputValue
        moveValue = Mathf.Lerp(moveValue, gear, smoothMovementFactor);
        transform.Translate(0f, moveValue * speed, 0f);

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
        if (turnValue != 0)
        {
            if (turnAcceleration < maxAcceleration)
                turnAcceleration += turnAccelerationSpeed;
        }
        // If not turning, then deaccelerate turning
        else if (turnAcceleration > 0)
        {
            turnAcceleration -= turnAccelerationSpeed;
        }
        //When acceleration goes negative, set i to 0
        else if (turnAcceleration <= 0)
        {
            turnAcceleration = 0;
        }

    }

    //Turns the ship smoothly
    void Turn()
    {
        //Smoothly change the turnValue to the inputValue
        turnValue = Mathf.Lerp(turnValue, turnInputValue, smoothTurningFactor);

        //Update turnDirection only when turning
        if (turnValue != 0)
            turnDirection = new Vector3(0f, 0f, turnValue);

        //Turn
        transform.Rotate(turnDirection * turnAcceleration);
    }

}

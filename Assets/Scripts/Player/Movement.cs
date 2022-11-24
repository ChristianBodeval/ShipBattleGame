using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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
    public int numberOfGears; // 2
    private float maxMovementSpeed; //0.11
    //public float moveDampener = 0.7F;

    //Holder values
    float moveInputValue;
    private float turnInputValue;
    private float latestInput;
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

    //Movement settings
    public enum MoveType {Gears, OnHold, Constant, ConstantAndStop};
    public MoveType moveType;

    //Dash settings
    public enum DashType {OnRelease, DoubleTap, OneTap};
    public DashType dashType;

    //Dash
    public bool isDashing;
    public bool canDash;
    public float dashTime;
    public int dashGearValue;
    public float dashCooldown;
    public InputActionReference actionReference;

    

    private void Start()
    {
        canDash = true;
        shipManager = GetComponent<ShipManager>();
        actionReference.action.started += context =>
        {
            //Debug.Log("Action called");
            if (context.interaction is MultiTapInteraction)
            {
                //Debug.Log("Dash");
                //Dash();
            }
        };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateTurnAcceleration();
        if(!isDashing)
            Turn();
        MoveForward();
    }

    private void OnEnable()
    {
        actionReference.action.Enable();
    }
    private void OnDisable()
    {
        actionReference.action.Disable();
    }

   

    //Sets a gear between 0 and maxGears. W for going up a gear, S for going down.  
    public void OnMove(InputAction.CallbackContext context)
    {

        moveInputValue = context.ReadValue<float>();

        // -----> Dash <---- //
        //Dash on Multitap
        if (latestInput > 0 && context.interaction is MultiTapInteraction && context.started && dashType == DashType.DoubleTap)
        {

            StartCoroutine(Dash());
        }
        //Dash on Release
        if (latestInput > 0 && dashType == DashType.OnRelease)
        {
            if (context.canceled && !isDashing && canDash)
            {

                StartCoroutine(Dash());
            }
        }

        if (latestInput > 0 && dashType == DashType.OneTap)
        {
            if (context.started && !isDashing && canDash)
            {


                StartCoroutine(Dash());
            }
        }


        // -----> MovementType <---- //
        //LatestInput, needed for knowing what was last pressedbecause when context.cancelled is true the moveInputValue is 0
        if (moveInputValue != 0)
        {
            if (moveInputValue > 0)
                latestInput = 1;
            else if (moveInputValue < 0)
                latestInput = -1;
        }


        //Constant move
        if (moveType == MoveType.Constant && !isDashing)
        {
            currentGear = 1;
        }

        //Constant move with stop
        if (moveType == MoveType.ConstantAndStop && !isDashing)
        {
            if (moveInputValue > 0)
            {
                currentGear = (int)moveInputValue;

            }
            else if (moveInputValue < 0)
            {
                currentGear = 0;
            }
            return;
        }

        //Move while holding
        if (moveType == MoveType.OnHold && !isDashing)
        {
            if(moveInputValue >= 0)
            {
                currentGear = (int)Mathf.Abs(latestInput);
                
            }
            else if (moveInputValue < 0 && context.started)
            {
                currentGear = 0;
            }
            return;
        }

        
        //Move dependent on gear
        if(moveType == MoveType.Gears)
        {
            if (currentGear < numberOfGears-1 && moveInputValue > 0)
            {
                currentGear++;
            }
            if (currentGear > 0 && moveInputValue < 0)
            {
                currentGear--;
            }         
        }
    }

    IEnumerator Dash ()
    {
        isDashing = true;
        canDash = false;
        currentGear = dashGearValue;
        yield return new WaitForSeconds(dashTime);

        if (moveType == MoveType.OnHold)
            currentGear = 0;
        else
        {
            currentGear = 1;
            currentTurnValue = 0;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }



    public void MoveForward()
    {
        //Smoothly change the turnValue to the inputValue
        currentMoveValue = Mathf.Lerp(currentMoveValue, currentGear, smoothMovementFactor);
        /*if (currentGear >= 3)
        {
            transform.Translate(0f, moveDampener*currentMoveValue * maxMovementSpeed, 0f);
            return;
        }*/
        transform.Translate(0f, currentMoveValue * maxMovementSpeed, 0f);
       

    }

    //Sets input value
    public void OnTurn(InputAction.CallbackContext context)
    {
        turnInputValue = context.ReadValue<float    >();
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

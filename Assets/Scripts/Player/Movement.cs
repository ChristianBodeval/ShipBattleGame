using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Movement : MonoBehaviour
{
    public GameObject playerPrefab;
    public ShipManager shipManager;
    public SpriteRenderer ramSprite;
    private Color ramSpriteColor;

    //All the following values are public, so i makes it easier to test in the inspector
    //Values which have an impact on the movement
    public float maxTurnSpeed; // 1.5
    public float turnAcceleration; // 0.8
    public float smoothTurningFactor; //0.1
    public float smoothMovementFactor; //0.1
    public int numberOfGears; // 2
    public float maxMovementSpeed; //0.11
    //public float moveDampener = 0.7F;

    //Holder values
    float moveInputValue;
    private float turnInputValue;
    private float latestInput;
    private Vector3 turnDirection;
    public float currentTurnAcceleration;
    public float currentTurnValue;
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
    public int dashGearValue;
    public bool isDashing;
    public bool canDash;
    public float dashTime;
    public float dashCooldown;
    public InputActionReference actionReference;

    //Sail
    public GameObject sail;



    private void Start()
    {
        ramSpriteColor = ramSprite.color;
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
        if (!isDashing)
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
        if (moveInputValue > 0 && context.interaction is MultiTapInteraction && context.started && dashType == DashType.DoubleTap)
        {
                
            StartCoroutine(Dash());
        }
        //Dash on Release // Uses latestInput since context.cancelled is not called on context.cancelled
        if (latestInput > 0 && dashType == DashType.OnRelease)
        {
            if (context.canceled && !isDashing && canDash)
            {

                StartCoroutine(Dash());
            }
        }

        if (moveInputValue > 0 && dashType == DashType.OneTap)
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
        BoxCollider2D ram = GetComponent<BoxCollider2D>();
        ram.enabled = true;
        currentTurnAcceleration = 0;
        currentTurnValue = 0;
        currentGear = dashGearValue;


       

        if (moveType == MoveType.OnHold)
            currentGear = 0;
        else
        {
            currentGear = 1;
        }

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        ram.enabled = false;

        /*
        float timer = Time.time + dashCooldown;

        float percentage = timer / Time.time;
        while (timer > Time.time)
        {
            percentage = Time.time / timer;
            Debug.Log("Calling");
            Debug.Log("Percentage: " + percentage);
            //ramSprite.color = Color.Lerp(Color.black, ramSprite.color, percentage * Time.deltaTime);

            ramSprite.color = new Color(1-percentage, 1-percentage, 1-percentage, 1);
            yield return null;
        }

        ramSprite.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        ramSprite.color = ramSpriteColor;
        */

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
        turnInputValue = context.ReadValue<float>();
    }

    /// <summary>
    /// Handles the acceleration of turning.
    /// </summary>
    void CalculateTurnAcceleration()
    {
        //Is player turning, then accelerate turning, since input value is either 1 or -1
        if (turnInputValue != 0 && currentTurnAcceleration < maxTurnSpeed)
        {
            currentTurnAcceleration += turnAcceleration * Time.deltaTime; 
        }
        // If not turning, then deaccelerate turning
        if (currentTurnAcceleration > 0 && turnInputValue == 0)
        {
            currentTurnAcceleration -= turnAcceleration * Time.deltaTime;
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
        
        
        // Sail rotation

        float sailRotation = sail.transform.localRotation.eulerAngles.z;


        if (sailRotation < 60 || sailRotation > 300)
        {

            if (turnInputValue > 0 && (sailRotation < 59 || sailRotation > 300))
            {
                sail.transform.Rotate(0f, 0f, 1f);
            }
            if (turnInputValue < 0 && (sailRotation > 301 || sailRotation < 60))
            {
                sail.transform.Rotate(0f, 0f, -1f);
            }

            if (turnInputValue == 0 && !Mathf.Approximately(sailRotation,0))
            {
                if (sailRotation < 179) {
                    sail.transform.Rotate(0f, 0f, -1f);
                }
                if (sailRotation > 179) {
                    sail.transform.Rotate(0f, 0f, 1f);
                }
            }
        }
        
    }

}

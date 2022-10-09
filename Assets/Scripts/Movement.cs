using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInput playerInput;

    //Rotation values
    public float turnInputValue;
    public float maxAcceleration = 5;
    public float startTurnAcceleration = 0.1F;
    public float currentTurnAcceleration;
    public float turnAccelerationSpeed;


    //Movement values
    public int gear = 0;
    public int maxGears = 2;
    public float speed = 5f;
    //For Turn 3


    public float movementFactor;
    PlayerInputActions playerInputActions;

    Vector3 rotationDirection;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Turn.performed += TurnOnPerformed;
        playerInputActions.Player.Accelerate.started += AccelerateOnStarted;

    }

    public void TurnOnPerformed(InputAction.CallbackContext context)
    {

        Debug.Log("Context: " + context);
    }

    public void AccelerateOnStarted(InputAction.CallbackContext context)
    {
        float horizontal = playerInputActions.Player.Accelerate.ReadValue<float>();

        if (gear < maxGears-1)
        {
            if (horizontal > 0)
                gear++;
        }
        if (gear > 0)
        {
            if (horizontal < 0)
                gear--;
        }

        Debug.Log("WD: "+ playerInputActions.Player.Accelerate.ReadValue<float>());
        Debug.Log("Gear: " + gear);


    }







    public void forwardSpeed1()
    {
        rb.AddForce(transform.up * speed * gear, ForceMode2D.Force);
    }

    public void forwardSpeed2()
    {
        movementFactor = Mathf.Lerp(movementFactor, gear, Time.deltaTime);
        transform.Translate(0f, movementFactor*speed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        Turn2();
        calculateAcceleration();
        forwardSpeed2();
    }


    /// <summary>
    /// Calculates increases rotationAcceleration when turning and deaccelerates when not turning
    /// </summary>
    void calculateAcceleration()
    {
        if (playerInputActions.Player.Turn.inProgress)
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

    void Turn1()

        //Note, proev at smooth inpurValue fra -1 til 1 til 0
    {

        turnInputValue = playerInputActions.Player.Turn.ReadValue<float>();

        if (playerInputActions.Player.Turn.inProgress)
            rotationDirection = new Vector3(0f, 0f, turnInputValue);
            transform.Rotate(rotationDirection * Time.deltaTime * currentTurnAcceleration);
        
    }


    
    void Turn2()
    {
        
        turnInputValue = Mathf.Lerp(turnInputValue, playerInputActions.Player.Turn.ReadValue<float>(), turnAccelerationSpeed);
        //turnInputValue = Mathf.SmoothStep(turnInputValue,playerInputActions.Player.Turn.ReadValue<float>(),5f);

        if (playerInputActions.Player.Turn.inProgress)
            rotationDirection = new Vector3(0f, 0f, turnInputValue);

        if (currentTurnAcceleration < 0.1)
            Debug.Log("Rotating");
            transform.Rotate(rotationDirection * Time.deltaTime * currentTurnAcceleration);

        


    }



}

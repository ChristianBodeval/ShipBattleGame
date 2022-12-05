using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SailRotation : MonoBehaviour
{
    float turnInputValue;

    float currentRotation = 0;
    float stopRotation;


    private void Start()
    {
        stopRotation = 60;
    }

    void Update()
    {

        

        currentRotation = transform.localEulerAngles.z;

        Debug.Log("Currentrotation: " + currentRotation);
        Debug.Log("Stoprotation" + stopRotation);
        Debug.Log("Called");
        Debug.Log(turnInputValue);

        if (turnInputValue == -1)
        {
            transform.Rotate(Vector3.forward,
                                       20 * Time.deltaTime);
        }
        else if (turnInputValue == 1)
        {
            transform.Rotate(Vector3.back,
                                        20 * Time.deltaTime);
        }



    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        turnInputValue = context.ReadValue<float>();
    }
}

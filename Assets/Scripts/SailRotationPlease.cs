using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SailRotationPlease : MonoBehaviour
{
    float turnInputValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public float OnTurn(InputAction.CallbackContext context) => turnInputValue = context.ReadValue<float>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Reference Sprite Renderer component
    private Renderer rend;

    // Color value set in Inspector (default white)
    [SerializeField]
    private Color colorTurnTo = Color.white;

    // Initialization
    private void Start()
    {
        // Assign Renderer component to rend variable
        rend = GetComponent<Renderer>();

        // Change sprite color to selected color
        rend.material.color = colorTurnTo;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUiScript : MonoBehaviour
{

    public TMP_Text     healthText;
    public PlayerHealth   playerHealth;
    private int i = 1;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        healthText.text = playerHealth.CurrentHealth.ToString();
    }
}

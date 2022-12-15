using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class characterStatsScreen : MonoBehaviour
{
    public Slider maxSpeed;
    public Slider handling;
    public TMP_Text health;
    public TMP_Text damage;
    public TMP_Text fireRateS;
    public TMP_Text rammingDamage;

    public ShipManager shipPrefab;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed.maxValue = 0.2f;
        maxSpeed.value = shipPrefab.MaxMovementSpeed;
        handling.maxValue = 0.17f;
        handling.value = shipPrefab.SmoothTurningFactor;
        health.text = shipPrefab.StartingHealth.ToString();
        damage.text = shipPrefab.TotalDamage.ToString();
        fireRateS.text = shipPrefab.FireRateInSeconds.ToString();
        rammingDamage.text = shipPrefab.RammingDamage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

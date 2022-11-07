using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    // This class is to manage various settings on a ship.
    [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the ship when it is created.

    [Header("Movement")]
    [SerializeField]
    private float maxTurnSpeed; // 1.5
    [SerializeField]
    private float turnAcceleration; // 0.8
    [SerializeField]
    private float smoothTurningFactor; //0.1
    [SerializeField]
    private float smoothMovementFactor; //0.1
    [SerializeField]
    private int numberOfGears; // 2
    [SerializeField]
    private float maxMovementSpeed; //0.11
    [Header("Health")]
    [SerializeField]
    private float startingHealth; //100
    [Header("Shooting")]
    [SerializeField]
    private float fireRateInSeconds;          // 1                                     // Firerate
    [SerializeField]
    private float totalDamage;
    [SerializeField]
    private float projectileSpeed;
    [Header("Knockback")]
    [SerializeField]
    private float knockbackValue;
    [SerializeField]
    private float smoothKnockbackFactor;
    [Header("ShooterGroup")]
    [Range(1, 20)]
    public float range;

    [Range(1, 10)]
    public int shooters;

    [Range(1, 10)]
    public float shipSize;

    [Range(0, 180)]
    public float maxAngle;

    [Range(-2, 0)]
    public float startCurve;


    private Health healthScript;
    private Movement movementScript;
    private Shooting shootingScript;
    private Knockback knockbackScript;

    private ShooterGroup[] shooterGroups; 

    private void Awake()
    {
        healthScript = GetComponent<Health>();
        movementScript = GetComponent<Movement>();
        shootingScript = GetComponent<Shooting>();
        knockbackScript = GetComponent<Knockback>();
        healthScript.StartingHealth = startingHealth;
        shooterGroups = GetComponentsInChildren<ShooterGroup>();
    }

    void UpdateValues()
    {
        //Movement
        movementScript.MaxTurnSpeed = maxTurnSpeed;
        movementScript.TurnAcceleration = turnAcceleration;
        movementScript.SmoothMovementFactor = smoothMovementFactor;
        movementScript.SmoothTurningFactor = smoothTurningFactor;
        movementScript.NumberOfGears = numberOfGears;
        movementScript.MaxMovementSpeed = maxMovementSpeed;
        //Health
        healthScript.StartingHealth = startingHealth;
        //Shooting
        shootingScript.FireRateInSeconds = fireRateInSeconds;
        shootingScript.ProjectileSpeed = projectileSpeed;
        shootingScript.TotalDamage = totalDamage;
        //Knockback
        knockbackScript.KnockbackValue = knockbackValue;
        knockbackScript.SmoothKnockbackFactor = smoothKnockbackFactor;
        //ShootingGroup e.g ConeShooting
        foreach (ShooterGroup s in shooterGroups)
        {
            s.Range = range;
            s.Shooters = shooters;
            s.ShipSize = shipSize;
            s.MaxAngle = maxAngle;
            s.StartCurve = startCurve;
        }
    }

    void Update()
    {
        UpdateValues();
    }
}

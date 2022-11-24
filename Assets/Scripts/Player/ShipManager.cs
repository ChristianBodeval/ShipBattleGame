using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipManager : MonoBehaviour
{
    private PlayerInput Input;




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
    [SerializeField]
    private float currentHealth;
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
    [Header("Ramming")]
    [SerializeField]
    private float rammingDamage;
    [Header("ShooterGroup")]
    [SerializeField]
    [Range(1, 25)]
    private float range;

    /*
    [Range(1, 25)]
    private float minRange;
    [Range(25, 50)]
    private float maxRange;
    */
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
    private Ramming rammingScript;

    private ShooterGroup[] shooterGroups;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float TotalDamage { get => totalDamage; set => totalDamage = value; }
    public float MaxMovementSpeed { get => maxMovementSpeed; set => maxMovementSpeed = value; }
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    /*
    public float MinRange { get => minRange; set => minRange = value; }
    public float MaxRange { get => maxRange; set => maxRange = value; }
    */


    private void Awake()
    {
        healthScript = GetComponent<Health>();
        movementScript = GetComponent<Movement>();
        shootingScript = GetComponent<Shooting>();
        knockbackScript = GetComponent<Knockback>();
        rammingScript = GetComponent<Ramming>();
        shooterGroups = GetComponentsInChildren<ShooterGroup>();
        healthScript.StartingHealth = startingHealth;
        healthScript.CurrentHealth = startingHealth;
        CurrentHealth = startingHealth;
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
        rammingScript.RammingDamage = rammingDamage;
        //Shooting
        shootingScript.FireRateInSeconds = fireRateInSeconds;
        shootingScript.ProjectileSpeed = projectileSpeed;
        shootingScript.TotalDamage = totalDamage;

        //Ramming
        rammingScript.RammingDamage = rammingDamage;
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





    private void Start()
    {
        Input = GetComponent<PlayerInput>();
    }

    private void OnDisable()
    {
        Input.actions = null;
    }




    void Update()
    {
        currentHealth = healthScript.CurrentHealth;

        UpdateValues();
    }
}

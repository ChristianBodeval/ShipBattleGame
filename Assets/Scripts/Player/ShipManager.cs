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
    private float turnAcceleration; // 0.08
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
    public bool fireOnBullets;
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
    private SpriteRenderer spriteRenderer;

    private ShooterGroup[] shooterGroups;
    private Color playerColor;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float TotalDamage { get => totalDamage; set => totalDamage = value; }
    public float MaxMovementSpeed { get => maxMovementSpeed; set => maxMovementSpeed = value; }
    public float StartingHealth { get => startingHealth; set => startingHealth = value; }
    public float Range { get => range; set => range = value; }
    public float RammingDamage { get => rammingDamage; set => rammingDamage = value; }
    public float MaxTurnSpeed { get => maxTurnSpeed; set => maxTurnSpeed = value; }
    public float TurnAcceleration { get => turnAcceleration; set => turnAcceleration = value; }
    public float SmoothTurningFactor { get => smoothTurningFactor; set => smoothTurningFactor = value; }
    public float SmoothMovementFactor { get => smoothMovementFactor; set => smoothMovementFactor = value; }
    public int NumberOfGears { get => numberOfGears; set => numberOfGears = value; }
    public float FireRateInSeconds { get => fireRateInSeconds; set => fireRateInSeconds = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float KnockbackValue { get => knockbackValue; set => knockbackValue = value; }
    public float SmoothKnockbackFactor { get => smoothKnockbackFactor; set => smoothKnockbackFactor = value; }



    float currentHealth_default;
    float totalDamage_default;
    float maxMovementSpeed_default;
    float startingHealth_default;
    float range_default;
    float rammingDamage_default;
    float maxTurnSpeed_default;
    float turnAcceleration_default;
    float smoothTurningFactor_default;
    float smoothMovementFactor_default;
    int numberOfGears_default;
    float fireRateInSeconds_default;
    float projectileSpeed_default;
    float knockbackValue_default;
    float smoothKnockbackFactor_default;
    //ShooterGroups
    bool fireOnBullets_default;
    int shooters_default;
    float shipSize_default;
    float maxAngle_default;
    float startCurve_default;

    public bool hasPowerUp;
    

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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthScript.StartingHealth = startingHealth;
        healthScript.CurrentHealth = startingHealth;
        CurrentHealth = startingHealth;
    }

    void Start()
    {
        hasPowerUp = false;
        Input = GetComponent<PlayerInput>();
        SetDefaultValues();
    }

    void SetDefaultValues()
    {
        currentHealth_default = currentHealth;
        totalDamage_default = totalDamage;
        maxMovementSpeed_default = maxMovementSpeed;
        startingHealth_default = startingHealth;
        rammingDamage_default = rammingDamage;
        maxTurnSpeed_default = maxMovementSpeed;
        turnAcceleration_default = turnAcceleration;
        smoothTurningFactor_default = smoothTurningFactor;
        smoothMovementFactor_default = smoothMovementFactor;
        numberOfGears_default = numberOfGears;
        fireRateInSeconds_default = fireRateInSeconds;
        projectileSpeed_default = projectileSpeed;
        knockbackValue_default = knockbackValue;
        smoothKnockbackFactor_default = smoothKnockbackFactor;

        range_default = range;
        shooters_default = shooters;
        shipSize_default = shipSize;
        maxAngle_default = maxAngle;
        startCurve_default = startCurve;
        fireOnBullets_default = fireOnBullets;
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
            s.FireOnBullets = fireOnBullets;

        }
    }


    public void Die()
    {
        //Input.actions = null;

        playerColor = spriteRenderer.color;
        spriteRenderer.color = Color.black;

        healthScript.enabled = false;
        movementScript.enabled = false;
        shootingScript.enabled = false;
        //knockbackScript.enabled = false;
        rammingScript.enabled = false;
        foreach (var shooterGroup in shooterGroups)
        {
            shooterGroup.enabled = false;
        }
    }

    public void ResetValues()
    {
        currentHealth = currentHealth_default;
        totalDamage = totalDamage_default;
        maxMovementSpeed = maxMovementSpeed_default;
        startingHealth = startingHealth_default;
        range = range_default;
        rammingDamage = rammingDamage_default;
        maxTurnSpeed = maxMovementSpeed_default;
        turnAcceleration = turnAcceleration_default;
        smoothTurningFactor = smoothTurningFactor_default;
        smoothMovementFactor = smoothMovementFactor_default;
        numberOfGears = numberOfGears_default;
        fireRateInSeconds = fireRateInSeconds_default;
        projectileSpeed = projectileSpeed_default;
        knockbackValue = knockbackValue_default;
        smoothKnockbackFactor = smoothKnockbackFactor_default;
        //ShootingGroup
        range = range_default;
        shooters = shooters_default;
        shipSize = shipSize_default;
        maxAngle = maxAngle_default;
        startCurve = startCurve_default;
        fireOnBullets = fireOnBullets_default;

    }

    public void Revive()
    {
        spriteRenderer.color = playerColor;
        healthScript.enabled = true;
        movementScript.enabled = true;
        shootingScript.enabled = true;
        //knockbackScript.enabled = true;
        rammingScript.enabled = true;
        foreach (var shooterGroup in shooterGroups)
        {
            shooterGroup.enabled = true;
        }
    }




    void Update()
    {
        currentHealth = healthScript.CurrentHealth;

        UpdateValues();
    }
}

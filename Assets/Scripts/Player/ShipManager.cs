using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


//Handles values of the player scripts & the state (alive or dead)

public class ShipManager : MonoBehaviour
{
    private PlayerInput Input;
    public Transform m_SpawnPoint;
   
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

    
    public bool fireOnBullets;
    [Range(1, 10)]
    public int shooters;

    [Range(1, 10)]
    public float shipSize;

    [Range(0, 180)]
    public float maxAngle;

    [Range(-2, 0)]
    public float startCurve;

    public bool shooterGroupsAreIdentical;
    public bool isDead;
    public int roundWins;

    private PlayerHealth healthScript;
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


    public float maxMovementSpeed_default; //Used for whirlpool

    /* Default Values */
    [HideInInspector] public float currentHealth_default;
    [HideInInspector] public float totalDamage_default;
    [HideInInspector] public float startingHealth_default;
    [HideInInspector] public float range_default;
    [HideInInspector] public float rammingDamage_default;
    [HideInInspector] public float maxTurnSpeed_default;
    [HideInInspector] public float turnAcceleration_default;
    [HideInInspector] public float smoothTurningFactor_default;
    [HideInInspector] public float smoothMovementFactor_default;
    [HideInInspector] public float fireRateInSeconds_default;
    [HideInInspector] public float projectileSpeed_default;
    [HideInInspector] public float knockbackValue_default;
    [HideInInspector] public float smoothKnockbackFactor_default;
    [HideInInspector] public int numberOfGears_default;
    //ShooterGroups
    [HideInInspector] public bool fireOnBullets_default;
    [HideInInspector] public int shooters_default;
    [HideInInspector] public float shipSize_default;
    [HideInInspector] public float maxAngle_default;
    [HideInInspector] public float startCurve_default;

    public bool hasPowerUp;
    public float timeToRespawn;

    private void Awake()
    {
        m_Instance = gameObject;
        healthScript = GetComponent<PlayerHealth>();
        movementScript = GetComponent<Movement>();
        shootingScript = GetComponent<Shooting>();
        knockbackScript = GetComponent<Knockback>();
        rammingScript = GetComponent<Ramming>();
        shooterGroups = GetComponentsInChildren<ShooterGroup>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthScript.StartingHealth = startingHealth;
        healthScript.CurrentHealth = startingHealth;
        CurrentHealth = startingHealth;
        playerColor = spriteRenderer.color;
    }

    void Start()
    {
        timeToRespawn = GameManager.Instance.timeToRespawn;
        hasPowerUp = false;
        Input = GetComponent<PlayerInput>();
        SetDefaultValues();
        UpdateValues();
    }

    void Update()
    {
        currentHealth = healthScript.CurrentHealth;
    }

    public void SetDefaultValues()
    {
        currentHealth_default = currentHealth;
        totalDamage_default = totalDamage;
        maxMovementSpeed_default = maxMovementSpeed;
        startingHealth_default = startingHealth;
        rammingDamage_default = rammingDamage;
        maxTurnSpeed_default = maxTurnSpeed;
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

    public void EnableScripts()
    {
        healthScript.enabled = true;
        movementScript.enabled = true;
        shootingScript.enabled = true;
        knockbackScript.enabled = true;
        rammingScript.enabled = true;
        foreach (var s in shooterGroups)
        {
            s.enabled = true;
        }
    }

    public void DisableScripts()
    {
        healthScript.enabled = false;
        movementScript.enabled = false;
        shootingScript.enabled = false;
        knockbackScript.enabled = false;
        rammingScript.enabled = false;
        foreach (var s in shooterGroups)
        {
            s.enabled = true;
        }
    }


    public void UpdateValues()
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
        //ShootingGroup
        foreach (ShooterGroup s in shooterGroups)
        {
            if(shooterGroupsAreIdentical)
            {
                s.Range = range;
                s.Shooters = shooters;
                s.ShipSize = shipSize;
                s.MaxAngle = maxAngle;
                s.StartCurve = startCurve;
                s.FireOnBullets = fireOnBullets;
            }
        }
    }

    public void Die()
    {
        isDead = true;
        SoundManager.Instance.PlayEffects("ShipBreak");
        DisableScripts();
    }

    public void ResetValues()
    {
        currentHealth = currentHealth_default;
        totalDamage = totalDamage_default;
        maxMovementSpeed = maxMovementSpeed_default;
        startingHealth = startingHealth_default;
        range = range_default;
        rammingDamage = rammingDamage_default;
        maxTurnSpeed = maxTurnSpeed_default;
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
        UpdateValues();
    }

    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(timeToRespawn);
        isDead = false;

        ResetValues();
        UpdateValues();
        SetDefaultValues();
        healthScript.StartingHealth = startingHealth_default;
        healthScript.CurrentHealth = currentHealth_default;
        m_Instance.transform.position = m_SpawnPoint.transform.position;
        m_Instance.transform.rotation = m_SpawnPoint.transform.rotation;
        healthScript.ResetColor();
        EnableScripts();

        yield return null;
    }
}

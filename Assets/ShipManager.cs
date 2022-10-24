using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    // This class is to manage various settings on a ship.
    [HideInInspector] public GameObject m_Instance;         // A reference to the instance of the ship when it is created.

    [SerializeField]
    private float maxAcceleration; // 1.5
    [SerializeField]
    private float turnAccelerationSpeed; // 0.8
    [SerializeField]
    private float smoothTurningFactor; //0.1
    [SerializeField]
    private float smoothMovementFactor; //0.1
    [SerializeField]
    private int gears; // 2
    [SerializeField]
    private float speed; //0.11
    [SerializeField]
    private float health; //100
    [SerializeField]
    private float cannonBallScaleMultiplier; // 1                                      // Make the size of cannonball bigger or smaller
    [SerializeField]
    private float fireRateInSeconds;          // 1                                     // Firerate
    [SerializeField]
    private float cannonBallForce;

    public float MaxAcceleration { get => maxAcceleration; set => maxAcceleration = value; }
    public float TurnAccelerationSpeed { get => turnAccelerationSpeed; set => turnAccelerationSpeed = value; }
    public float SmoothTurningFactor { get => smoothTurningFactor; set => smoothTurningFactor = value; }
    public float SmoothMovementFactor { get => smoothMovementFactor; set => smoothMovementFactor = value; }
    public int Gears { get => gears; set => gears = value; }
    public float Speed { get => speed; set => speed = value; }
    public float CannonBallScaleMultiplier { get => cannonBallScaleMultiplier; set => cannonBallScaleMultiplier = value; }
    public float FireRateInSeconds { get => fireRateInSeconds; set => fireRateInSeconds = value; }
    public float Health { get => health; set => health = value; }
    public float CannonBallForce { get => cannonBallForce; set => cannonBallForce = value; }
}

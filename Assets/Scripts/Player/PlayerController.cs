using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Assets.Scripts;
using Assets.Scripts.HitDetection;
using Assets.Scripts.Conditions.Burning;

[RequireComponent(typeof(CharacterController))]
public partial class PlayerController : MonoBehaviour, IHitCallback, IBurnable  {
    [SerializeField] private float batteryCapacity;
    [SerializeField] private float powerConsumption;
    [SerializeField] private float mobility;
    [SerializeField] private float resistance;

    [Header("Burn Properties")]
    [SerializeField] private bool burnImmunity = false;
    [SerializeField] private float burnThreshold = 10f;
    [SerializeField] private float burnResistance = 1f;
    [SerializeField] private float burnRecovery = 3f;
    [Space]

    [SerializeField] private ProgressBar charge;

    [SerializeField] private Battery battery;

    [Header("Lives")]
    [SerializeField] private int lives = 0;

    private Vector2 _lookValue;

    private PlayerInput _playerInput;
    
    private Character _character;
    private Weapon _weapon;

    private InteractionController _interactionController;
    private NavMeshAgent _navMeshAgent;

    [Header("Movement")]
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxSpeed;
    private Vector3 _velocity;
    private Vector3 _desiredVelocity;
    private Vector2 _moveDirection;
    private CharacterController _characterController;

    private void Awake() {
        _characterController = GetComponent<CharacterController>();

        _playerInput = GetComponent<PlayerInput>();
        _interactionController = GetComponent<InteractionController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updatePosition = false;
        
        battery.maxCharge = batteryCapacity;
    }

    private void Start() {
        battery.charge = batteryCapacity * 0.5f;
    }

    private void Update() {
        Move();
        BatteryDecay();
    }

    private void Move() {
        _velocity = _characterController.velocity;
        
        _desiredVelocity.x = _moveDirection.x * maxSpeed;
        _desiredVelocity.z = _moveDirection.y * maxSpeed;
        
        var maxSpeedChange = maxAcceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, maxSpeedChange);
        _velocity.z = Mathf.MoveTowards(_velocity.z, _desiredVelocity.z, maxSpeedChange);
        _velocity.y += Physics.gravity.y * Time.deltaTime;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void BatteryDecay() {
        battery.RemoveCharge(powerConsumption * Time.deltaTime);
        if (battery.Depleted) {
            // TODO: Destroy with message battery depleted
        }
    }

    public void OnHit(HitData hitData) {
        battery.RemoveCharge(hitData.damage);
    }

    #region Burnable Interface

    public float BurnThreshold => burnThreshold;

    public float BurnResistance => burnResistance;

    public float BurnRecovery => burnRecovery;

    public bool CanBurn() {
        return !burnImmunity;
    }

    public void BurnTick() {
        battery.RemoveCharge(10f * Time.deltaTime);
        if (battery.Depleted) {
            // TODO: destroy with burn message
            Debug.Log("Player killed by burn");
        }
    }

    #endregion

    #region InputHandling

    public void OnMove(InputAction.CallbackContext value) {
        _moveDirection = value.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext value) {
        _lookValue = value.ReadValue<Vector2>();
        _lookValue.x -= Screen.width * 0.5f;
        _lookValue.y -= Screen.height * 0.5f;
    }

    public void OnInteract(InputAction.CallbackContext value) {
        if(value.started)
            _interactionController.StartInteraction();
    }

    public void OnFire(InputAction.CallbackContext value) {
        if (value.started)
            _weapon.Fire();
    }

    public void SetCharacter(Character character) {
        _character = character;
    }

    public void SetWeapon(Weapon weapon) {
        _weapon = weapon;
    }

    #endregion
}

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Assets.Scripts;
using Assets.Scripts.HitDetection;
using Assets.Scripts.Movement;
using hinos;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput), typeof(RigidMovingController))]
public partial class PlayerController : MonoBehaviour, IStateMachine<PlayerController>, IHitCallback  {
    [SerializeField] private float mainHealthCapacity;
    [SerializeField] private float altHealthCapacity;

    [Header("Burn Properties")]
    [SerializeField] private bool burnImmunity = false;
    [SerializeField] private float burnThreshold = 10f;
    [SerializeField] private float burnResistance = 1f;
    [SerializeField] private float burnRecovery = 3f;
    private float burnValue = 0;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float maxAcceleration = 10f;

    class PlayerInputHandler {
        public readonly PlayerInput input;
        public readonly InputAction moveAction;
        public readonly InputAction lookAction;
        public readonly InputAction interactInputAction;

        public PlayerInputHandler(PlayerInput input) {
            this.input = input;

            moveAction = input.actions["Move"];
            lookAction = input.actions["Look"];
            interactInputAction = input.actions["Interact"];
        }

    }

    private PlayerInputHandler playerInput;
    private RigidMovingController _movementController;

    // State
    private PlayerStateFactory stateFactory;
    private State<PlayerController> currentState;

    // Health
    private ResourcePool mainHealthPool;
    private ResourcePool altHealthPool;

    // Interaction
    private Interactable interactable;
    private bool interacting;
    private float holdTime;

    private void Awake() {
        _movementController = GetComponent<RigidMovingController>();

        // State
        stateFactory = new PlayerStateFactory(this);
        currentState = stateFactory.GetIdleState();

        // Health
        mainHealthPool = new ResourcePool(mainHealthCapacity, mainHealthCapacity);
        altHealthPool = new ResourcePool(altHealthCapacity, altHealthCapacity);

        // Input
        var c_PlayerInput = GetComponent<PlayerInput>();
        playerInput = new PlayerInputHandler(c_PlayerInput);
    }

    private void Update() {
        ProcessInteraction();
        ProcessTargeting();
        ProcessMovement();
        currentState.Update();
    }

    private void ProcessInteraction() {
        if (playerInput.interactInputAction.WasPressedThisFrame()) {
            interacting = true;
            holdTime = 0f;
        }

        if (playerInput.interactInputAction.WasReleasedThisFrame()) {
            interacting = false;
            holdTime = 0f;
        }

        if (interacting) {
            if (interactable.HoldInteract) {
                holdTime += Time.deltaTime;

                if (holdTime > interactable.HoldDuration) {
                    interactable.Interact(this.gameObject);
                    interacting = false;
                }
            }
            else {
                interactable.Interact(this.gameObject);
                interacting = false;
            }
        }
    }

    private void ProcessTargeting() {
        var lookInput = playerInput.lookAction.ReadValue<Vector2>();
        lookInput.x -= Screen.width * 0.5f;
        lookInput.y -= Screen.height * 0.5f;
    }

    private void ProcessMovement() {
        var moveInput = playerInput.moveAction.ReadValue<Vector2>();
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);
        _movementController.Move(moveInput, maxSpeed, maxAcceleration);
    }

    public void OnHit(HitData hitData) {
        ProcessDamage(hitData.damage, hitData.element);

        switch (hitData.element) {
            case ElementType.HEAT:
                ProcessHeatEnergy(hitData.damage);
                break;
            default:
                break;
        }
    }

    private void ProcessDamage(float damage, ElementType type) {
        if (damage <= 0) return;

        //TODO: Apply damage modifiers

        var rest = -damage;

        if (rest < 0 && mainHealthPool.Value > 0) {
            rest = mainHealthPool.ApplyValue(rest);
            if(rest < 0) {
                //TODO: Handle main health pool depleted
            }
        }

        if (rest < 0 && altHealthPool.Value > 0) {
            rest = altHealthPool.ApplyValue(rest);
            if(rest < 0) {
                //TODO: Handle alt health pool depleted
            }
        }

        if (rest < 0) {
            //TODO: Handle player death

            return; //WARNING: Dont execute the following lines
            HandleSwitchState(currentState, stateFactory.GetDeadState());
        }
    }

    private void ProcessHeatEnergy(float amount) {
        if (amount < 0) return;

        burnValue += amount * burnResistance;
        if(burnValue >= burnThreshold) {
            //TODO: Proc burn effect
        }
    }

    public void HandleSwitchState(State<PlayerController> oldState, State<PlayerController> newState) {
        oldState.Exit();
        newState.Enter();
        currentState = newState;
    }
}

public class Interactable : MonoBehaviour {
    [SerializeField] private bool holdInteract = true;
    [SerializeField] private float holdDuration = 1f;
    [SerializeField] private bool multipleUse = false;
    [SerializeField] private bool isInteractable = true;

    public InteractionUnityEvent interactionEvent = new();

    public bool IsInteractable { get => isInteractable; }
    public bool HoldInteract { get => holdInteract; }
    public float HoldDuration { get => holdDuration; }

    public void Interact(GameObject source) {
        interactionEvent.Invoke(source);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace hinos.player
{
    public class PlayerInputProcessor : MonoBehaviour, DefaultActions.IPlayerActions
    {
        public Transform inputSpace;

        // Processed Values
        private Vector3 _moveDirection;

        // Input
        private DefaultActions _actions;
        private Vector2 _moveInputDirection;
        private Vector2 _targetInputDirection;

        private bool _fireHeld;
        private float _fireHoldTime;

        public Vector3 MoveDirection {
            get => _moveDirection;
        }

        private void Awake() {
            _actions = new DefaultActions();
            _actions.Player.SetCallbacks(this);
        }

        private void OnEnable() {
            _actions.Player.Enable();
        }

        private void OnDisable() {
            _actions.Player.Disable();
        }

        private void Update() {
            // Calculate move direction
            if(inputSpace != null) {
                var forward = SimpleProjection(inputSpace.forward);
                var right = SimpleProjection(inputSpace.right);
                _moveDirection = forward * _moveInputDirection.y + right * _moveInputDirection.x;
            }
            else {
                _moveDirection = new Vector3(_moveInputDirection.x, 0, _moveInputDirection.y);
            }

            // Process fire input
            if(_fireHeld) {
                _fireHoldTime += Time.deltaTime;
            }
        }

        private Vector3 SimpleProjection(Vector3 vector) {
            vector.y = 0;
            return vector.normalized;
        }

        public void OnFire(InputAction.CallbackContext context) {
            if(context.started) {
                _fireHeld = true;
                _fireHoldTime = 0;
            }
            else if(context.canceled) {
                _fireHeld = false;
            }
        }

        public void OnInteract(InputAction.CallbackContext context) {
            
        }

        public void OnMove(InputAction.CallbackContext context) {
            _moveInputDirection = context.ReadValue<Vector2>();
        }

        public void OnTurn(InputAction.CallbackContext context) {
            _targetInputDirection = context.ReadValue<Vector2>();
        }
    }
}

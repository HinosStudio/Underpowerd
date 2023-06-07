using UnityEngine;
using hinos.topdown;

namespace hinos.player
{
    public class PlayerCharacter : MonoBehaviour
    {
        
        // Components
        private PlayerInputProcessor _inputProcessor;
        private TopDownController _topDownController;

        private void Awake() {
            _inputProcessor = GetComponent<PlayerInputProcessor>();
            _topDownController = GetComponent<TopDownController>();
        }

        private void Update() {
            _topDownController.MoveTowards(_inputProcessor.MoveDirection);
        }
    }
}

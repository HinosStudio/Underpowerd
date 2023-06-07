using UnityEngine;

namespace hinos.underpowerd
{
    public class EnergyResource : MonoBehaviour
    {
        public float capacity;
        public bool startWithInitialCapacity;
        
        private float _value;

        public event System.Action OnValueChangedEvent;

        public float Value => _value;

        private void Start() {
            if(startWithInitialCapacity) {
                _value = capacity;
            }
        }

        public void ApplyValue(float value) {
            _value += value;
            OnValueChangedEvent?.Invoke();
        }
    }
}
using System;
using UnityEngine;

namespace Hinos.Energy {
    public class EnergyStore : MonoBehaviour {
        public float capacity;

        private float value;

        public event Action OnStoreDepletedEvent;
        public event Action OnStoreAugmentedEvent;

        public float ApplyCharge(float inputValue) {
            float delta = this.value + inputValue;

            if(delta > capacity) {
                this.value = capacity;
                OnStoreAugmentedEvent?.Invoke();
                return delta - capacity;
            }

            if(delta < 0) {
                this.value = 0;
                OnStoreDepletedEvent?.Invoke();
                return delta;
            }

            this.value = delta;
            return 0;
        }

        public static void TransferValue(EnergyStore source, EnergyStore target, float value) {

        }
    }
}
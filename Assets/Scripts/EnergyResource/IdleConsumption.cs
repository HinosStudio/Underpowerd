using UnityEngine;

namespace hinos.underpowerd
{
    public class IdleConsumption : MonoBehaviour
    {
        public float consumptionAmount;
        public bool isConsuming = true;

        private EnergyResource _resource;

        private void Awake() {
            _resource = GetComponent<EnergyResource>();
        }

        private void Update() {
            if(isConsuming) {
                var amount = consumptionAmount * Time.deltaTime;
                _resource.ApplyValue(-amount);
            }
        }
    }
}
using UnityEngine;

namespace Assets.Scripts.Conditions.Burning {
    public class Burnable : MonoBehaviour{
        private IBurnable _burnable;
        private float _value = 0;
        private bool _burning = false;

        private void Awake() {
            _burnable = GetComponent<IBurnable>();
        }

        private void Update() {
            if (_burning) {
                _burnable.BurnTick();
            }

            _value -= _burnable.BurnThreshold / _burnable.BurnRecovery * Time.deltaTime;
            if(_value < 0) {
                _value = 0;
                _burning = false;
            }
        }

        public void OnHit(HitData data) {
            if (_burning || data.element != ElementType.HEAT) return;

            _value += data.damage * _burnable.BurnResistance;
            if(_value >= _burnable.BurnThreshold) {
                _value = _burnable.BurnThreshold;
                
                if(_burnable.CanBurn())
                    _burning = true;
            }
        }
    }
}

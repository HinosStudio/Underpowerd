using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace hinos.underpowerd
{
    public class GUIPlayerHealth : MonoBehaviour
    {
        public TMP_Text text;
        public EnergyResource resource;

        private void OnEnable() {
            resource.OnValueChangedEvent += OnValueChanged;
        }

        private void OnDisable() {
            resource.OnValueChangedEvent -= OnValueChanged;
        }

        private void OnValueChanged() {
            text.text = $"Energy: {resource.Value:0.##}";
        }
    }
}
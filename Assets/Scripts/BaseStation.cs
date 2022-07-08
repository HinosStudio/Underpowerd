using Assets.Scripts.Interaction;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class BaseStation : MonoBehaviour, IInteractable, IConnectCallback {
        [SerializeField] private Battery battery;
        [SerializeField] private ConnectionPoint connectionPoint;
        [SerializeField] private RegisterInteractableTriggerArea triggerArea;

        private readonly Dictionary<Connector, Battery> _connectedBatteries = new Dictionary<Connector, Battery>();

        private void Awake() {
            triggerArea.interactable = this;
        }

        private void Update() {
            TransferCharge();
            CheckDistance();
        }

        private void TransferCharge() {
            foreach (Battery bat in _connectedBatteries.Values) {
                Battery.TransferCharge(bat, battery, Time.deltaTime);
            }
        }

        private void CheckDistance() {

        }

        public void Interact(GameObject src) {
            var connector = src.GetComponent<Connector>();
            if (connector) {
                connector.ConnectToObject(connectionPoint);
            }
        }

        public void OnConnect(Connector other) {
            if (!_connectedBatteries.ContainsKey(other)) {

            }
        }

        public void OnDisconnect(Connector other) {
            
        }
    }
}

using Hinos.Energy;
using UnityEngine;

namespace Hinos.Connections {
    public abstract class Connection {
        protected readonly GameObject source;
        protected readonly GameObject target;

        protected Connection(GameObject source, GameObject target) {
            this.source = source;
            this.target = target;
        }

        public abstract void Update();
    }

    public class EnergyTransferConnection : Connection {
        private readonly EnergyStore sourceBattery;
        private readonly EnergyStore targetBattery;

        public EnergyTransferConnection(GameObject source, GameObject target) : base(source, target) {
            this.sourceBattery = source.GetComponent<EnergyStore>();
            this.targetBattery = target.GetComponent<EnergyStore>();
        }

        public override void Update() {
            EnergyStore.TransferValue(sourceBattery, targetBattery, Time.deltaTime);
        }
    }
}

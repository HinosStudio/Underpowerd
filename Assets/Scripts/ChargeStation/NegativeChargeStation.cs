using UnityEngine;

public class NegativeChargeStation : ChargeStation {

    public override void TransferCharge() {
        Battery.TransferCharge(targetBattery, m_Battery, Time.deltaTime);
    }
}

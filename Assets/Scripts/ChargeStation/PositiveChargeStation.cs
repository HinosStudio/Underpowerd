using UnityEngine;

public class PositiveChargeStation : ChargeStation {

    public override void TransferCharge() {
        Battery.TransferCharge(m_Battery, targetBattery, Time.deltaTime);
    }
}

using UnityEngine;

[RequireComponent(typeof(Character), typeof(Battery))]
public class PowerConsumingCharacter : MonoBehaviour {

    private Character m_Character;
    private Battery m_Battery;

    private void Awake() {
        m_Character = GetComponent<Character>();
        m_Battery = GetComponent<Battery>();
    }

    private void Update() {
        m_Battery.RemoveCharge(m_Character.PowerConsumption * Time.deltaTime);
    }
}
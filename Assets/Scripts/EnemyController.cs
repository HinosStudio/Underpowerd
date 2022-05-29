using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {
    [SerializeField] private Transform target;

    private NavMeshAgent m_NavMeshAgent;

    private void Awake() {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        m_NavMeshAgent.SetDestination(target.position);
    }
}

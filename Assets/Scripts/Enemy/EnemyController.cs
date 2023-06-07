using System;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {
    [SerializeField] private Transform target;


    private NavMeshAgent _navMeshAgent;


    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();

    }

    public void MoveTowardsTarget() {
        _navMeshAgent.SetDestination(target.position);
    }
}

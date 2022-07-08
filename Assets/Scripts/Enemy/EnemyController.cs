using Assets.Scripts.StatePattern;
using System;
using UnityEngine;
using UnityEngine.AI;

using EnemyState = IState<EnemyController>;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {
    [SerializeField] private Transform target;

    private StateMachine<EnemyController> _stateMachine;

    private NavMeshAgent _navMeshAgent;

    public StateMachine<EnemyController> StateMachine => _stateMachine;

    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _stateMachine = new StateMachine<EnemyController>(this);
        _stateMachine.CurrentState = new ChasingEnemyState();
    }

    private void Update() {
        _stateMachine.Update();
    }

    public void MoveTowardsTarget() {
        _navMeshAgent.SetDestination(target.position);
    }
}

public class ChasingEnemyState : EnemyState {
    public void OnEnter(EnemyController source) {
        
    }

    public void OnExit(EnemyController source) {
        source.MoveTowardsTarget();
    }

    public void OnUpdate(EnemyController source) {

    }
}

public class AttackingEnemyState : EnemyState {
    public void OnEnter(EnemyController source) {

    }

    public void OnExit(EnemyController source) {

    }

    public void OnUpdate(EnemyController source) {
        
    }
}

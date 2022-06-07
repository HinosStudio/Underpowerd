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

public class StateMachine {
    private EnemyState m_GlobalState;
    private EnemyState m_CurrentState;
    
    public void Update(EnemyController source) {
        m_GlobalState.Update(source);
        m_CurrentState.Update(source);
    }

    public void ChangeState(EnemyController source, EnemyState state) {
        m_CurrentState?.OnExit(source);
        m_CurrentState = state;
        m_CurrentState?.OnEnter(source);
    }
}

public class EnemyState {

    public virtual void OnEnter(EnemyController source) { }

    public virtual void OnExit(EnemyController source) { }

    public virtual void Update(EnemyController source) { }
}

public class ChasingEnemyState : EnemyState {

    public override void Update(EnemyController source) {

    }
}

public class AttackingEnemyState : EnemyState {

    public override void Update(EnemyController source) {
        
    }
}

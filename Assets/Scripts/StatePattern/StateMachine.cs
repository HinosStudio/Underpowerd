using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StatePattern {
    public class StateMachine<TType> {
        private TType _source;

        private IState<TType> _currentState;
        private IState<TType> _previousState;
        private IState<TType> _globalState;

        public IState<TType> CurrentState {
            get => _currentState;
            set => _currentState = value;
        }

        public IState<TType> PreviousState {
            get => _previousState;
            set => _previousState = value;
        }

        public IState<TType> GlobalState {
            get => _globalState;
            set => _globalState = value;
        }

        public StateMachine(TType source) {
            _source = source;
        }

        public void Update() {
            if (_globalState != null) _globalState.OnUpdate(_source);
            if(_currentState != null) _currentState.OnUpdate(_source);
        }

        public void ChangeState(IState<TType> newState) {
            if (newState == null) throw new NullReferenceException("Cannot assign null value to current state");

            _previousState = _currentState;

            _currentState.OnExit(_source);
            _currentState = newState;
            _currentState.OnEnter(_source);
        }

        public void RestorePreviousState() {
            ChangeState(_previousState);
        }

        public bool InState(IState<TType> state) {
            return _currentState == state;
        }
    }
}

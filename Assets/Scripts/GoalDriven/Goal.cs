using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GoalDriven {
    public abstract class Goal<T> {
        protected T _source;
        protected GoalStatus _status;
        protected readonly List<Goal<T>> _subgoals = new();

        public Goal(T source) {
            _source = source;
        }

        public bool IsActive() => _status == GoalStatus.ACTIVE;
        public bool IsInactive() => _status == GoalStatus.INACTIVE;
        public bool IsComplete() => _status == GoalStatus.COMPLETED;
        public bool HasFailed() => _status == GoalStatus.FAILED;

        public abstract void Activate();
        public abstract GoalStatus Process();
        public abstract void Terminate();
        public abstract bool HandleMessage();

        public abstract string GetDescription();
        public virtual int GetChildCount() => 0; 
    }

    public abstract class CompoundGoal<T> : Goal<T> {

        public CompoundGoal(T source) : base(source) {

        }

        public GoalStatus ProcessSubgoals() {
            while(_subgoals.Count > 0 && (_subgoals[0].IsComplete() || _subgoals[0].HasFailed())){
                _subgoals[0].Terminate();
                _subgoals.RemoveAt(0);
            }

            if(_subgoals.Count > 0) {
                var status = _subgoals[0].Process();
                if(status == GoalStatus.COMPLETED && _subgoals.Count > 1)
                    return GoalStatus.ACTIVE;
                return status;
            }

            return GoalStatus.COMPLETED;
        }

        public void AddSubgoal(Goal<T> goal) {
            _subgoals.Insert(0, goal); //push front
        }

        public void RemoveAllSubgoals() {
            _subgoals.ForEach((g) => g.Terminate());
            _subgoals.Clear();
        }
    }
}

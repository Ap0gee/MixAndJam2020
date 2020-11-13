using UnityEngine;
using System.Collections;

namespace GameJam.Core.State {

    public class State
    {
        protected FiniteStateMachine stateMachine;

        public State(FiniteStateMachine fsm)
        {
            stateMachine = fsm;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }
    }
}


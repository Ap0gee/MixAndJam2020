using System.Collections.Generic;

namespace GameJam.Core.State {

    public class FiniteStateMachine
    {
        protected Dictionary<int, State> states = new Dictionary<int, State>();
        protected State currentState;
       
        public void AddState(int key, State state)
        {
            states.Add(key, state);
        }

        public State GetState(int key)
        {
            return states[key];
        }

        public void SetState(State state)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = state;

            currentState.Enter();
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public void FixedUpdate()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }
    }
}
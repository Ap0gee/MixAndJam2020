using UnityEngine;
using GameJam.Core.State;

namespace GameJam.Managers._UI.States {
    public class Load : State
    {
        public Load(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)UIManager.UIState.Run);
            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
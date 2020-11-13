using UnityEngine;
using GameJam.Core.State;
using GameJam.Managers;

namespace GameJam.Managers._UI.States
{
    public class Reset : State
    {
        public Reset(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)UIManager.UIState.Load);
            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
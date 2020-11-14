using UnityEngine;
using GameJam.Core.State;

namespace GameJam.Managers._Game.States
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
            State nextState = stateMachine.GetState((int)GameManager.GameState.Unload);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
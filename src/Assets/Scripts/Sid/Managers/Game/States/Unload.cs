using UnityEngine;
using GameJam.Core.State;

namespace GameJam.Managers._Game.States
{
    public class Unload : State
    {
        public Unload(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)GameManager.GameState.Load);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
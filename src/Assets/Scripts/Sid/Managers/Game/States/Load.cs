using UnityEngine;
using GameJam.Core.State;

namespace GameJam.Managers._Game.States
{
    public class Load : State
    {
        public Load(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            if ( ! DataManager.LoadGame() )
            {
                DataManager.ResetSaveData();
                DataManager.SaveGame();
            }

            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)GameManager.GameState.Run);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
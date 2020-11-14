using UnityEngine;
using GameJam.Core.State;

namespace GameJam.Managers._Game.States
{
    public class Pause : State
    {
        public Pause(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            InputManager.PauseInput();
            Time.timeScale = 0;

            base.Enter();
        }

        public override void Update()
        {
            //Use Base Input Manager here because our InputManager has input paused.
            if (Input.GetButtonDown("Cancel"))
            {
                GameManager.UnPauseGame();
            }

            if ( ! GameManager.IsPaused )
            {
                State nextState = stateMachine.GetState((int)GameManager.GameState.Run);

                stateMachine.SetState(nextState);
            }

            base.Update();
        }
    }
}
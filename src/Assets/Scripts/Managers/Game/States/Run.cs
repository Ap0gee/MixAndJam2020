using UnityEngine;
using GameJam.Core.State;
using GameJam.Managers;

namespace GameJam.Managers._Game.States
{
    public class Run : State
    {
        public Run(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            InputManager.UnPauseInput();
            Time.timeScale = GameManager.TimeScale;

            base.Enter();
        }

        public override void Update()
        {
            if (InputManager.GetButtonDown("Cancel"))
            {
                GameManager.PauseGame();
            }

            if (GameManager.IsPaused)
            {
                State nextState = stateMachine.GetState((int)GameManager.GameState.Pause);

                stateMachine.SetState(nextState);
            }

            base.Update();
        }
    }
}
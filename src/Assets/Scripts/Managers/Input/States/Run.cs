using GameJam.Core.State;

namespace GameJam.Managers._Input.States
{
    public class Run : State
    {
        public Run(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            if (InputManager.IsPaused)
            {
                State nextState = stateMachine.GetState((int)InputManager.InputState.Pause);

                stateMachine.SetState(nextState);
            }
          
            base.Update();
        }
    }
}
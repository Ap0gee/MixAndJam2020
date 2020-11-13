using GameJam.Core.State;

namespace GameJam.Managers._Input.States
{
    public class Pause : State
    {
        public Pause(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            InputManager.SetPlayerControllerPaused(true);
            base.Enter();
        }

        public override void Update()
        {
            if ( ! InputManager.IsPaused )
            {
                State nextState = stateMachine.GetState((int)InputManager.InputState.Run);

                stateMachine.SetState(nextState);
            }
            
            base.Update();
        }
    }
}
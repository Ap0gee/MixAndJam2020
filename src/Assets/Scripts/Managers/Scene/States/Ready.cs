using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class Ready : State
    {
        public Ready(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            System.GC.Collect();

            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Run);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class Reset : State
    {
        public Reset(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            System.GC.Collect();

            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Preload);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
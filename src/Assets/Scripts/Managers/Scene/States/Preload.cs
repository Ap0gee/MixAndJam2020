using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class Preload : State
    {
        public Preload(FiniteStateMachine stateMachine) : base(stateMachine) { }        

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Load);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
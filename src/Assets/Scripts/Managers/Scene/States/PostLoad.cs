using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class PostLoad : State
    {
        public PostLoad(FiniteStateMachine stateMachine) : base(stateMachine) { }
       
        public override void Enter()
        {
            string eventName = SceneStateManager.EventNames.FinalizeSceneSwitch;
            EventManager.TriggerEvent(eventName);

            base.Enter();
        }

        public override void Update()
        {
            State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Ready);

            stateMachine.SetState(nextState);

            base.Update();
        }
    }
}
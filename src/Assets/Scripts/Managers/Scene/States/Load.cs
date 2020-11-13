using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class Load : State
    {
        public Load(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            string eventName = SceneStateManager.EventNames.LoadNextScene;
            EventManager.TriggerEvent(eventName);

            base.Enter();
        }

        public override void Update()
        {
            if (SceneStateManager.IsDoneLoading)
            {
                State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Unload);

                stateMachine.SetState(nextState);
            }

            base.Update();
        }
    }
}
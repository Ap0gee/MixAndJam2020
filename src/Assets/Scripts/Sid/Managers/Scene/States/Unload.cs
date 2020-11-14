using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class Unload : State
    {
        public Unload(FiniteStateMachine stateMachine) : base(stateMachine) { }
       
        public override void Enter()
        {
            string eventName = SceneStateManager.EventNames.UnloadUnusedAssets;
            EventManager.TriggerEvent(eventName);

            base.Enter();
        }

        public override void Update()
        {
            if (SceneStateManager.IsDoneUnloading)
            {
                State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Postload);

                stateMachine.SetState(nextState);
            }

            base.Update();
        }
    }
}
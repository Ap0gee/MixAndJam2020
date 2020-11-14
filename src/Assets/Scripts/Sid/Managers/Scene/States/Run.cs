using GameJam.Core.State;

namespace GameJam.Managers._Scene.States
{
    public class Run : State
    {
        public Run(FiniteStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            EventManager.TriggerEvent(SceneStateManager.EventNames.FinalizeSceneSwitch);

            System.GC.Collect();

            base.Enter();
        }
        
        public override void Update()
        {
            if (SceneStateManager.IsResetReady)
            {
                State nextState = stateMachine.GetState((int)SceneStateManager.SceneState.Reset);

                stateMachine.SetState(nextState);
            }

            base.Update();
        }
    }
}
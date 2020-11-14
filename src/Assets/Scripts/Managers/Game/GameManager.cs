using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameJam.Core.State;
using GameJam.Managers._Game.States;

namespace GameJam.Managers {

    public class GameManager : MonoBehaviour
    {   
        public static GameManager instance;

        private FiniteStateMachine stateMachine;

        private float timeScale = 1.0f;
        private bool isPaused = false;

        private readonly GameState gameState;

        private static FiniteStateMachine StateMachine
        {
            get { return instance.stateMachine; }
            set { instance.stateMachine = value; }
        }

        public enum GameState
        {
            Reset,
            Load,
            Unload,
            Run,
            Pause,
        }

        public struct EventNames { };

        public static bool IsPaused
        {
            get { return instance.isPaused; }
            private set { instance.isPaused = value; }
        }

        public static float TimeScale
        {
            get { return instance.timeScale; }
        }

        public static void PauseGame()
        {
            IsPaused = true;
        }

        public static void UnPauseGame()
        {
            IsPaused = false;
        }

        public static void TogglePauseGame()
        {
            IsPaused = !IsPaused;
        }

        private void HookEvents() { }

        private void UnhookEvents() { }

        public void Start()
        {
            StateMachine = new FiniteStateMachine();

            StateMachine.AddState((int)GameState.Reset, new Reset(StateMachine));
            StateMachine.AddState((int)GameState.Unload, new Unload(StateMachine));
            StateMachine.AddState((int)GameState.Load, new Load(StateMachine));
            StateMachine.AddState((int)GameState.Run, new Run(StateMachine));
            StateMachine.AddState((int)GameState.Pause, new Pause(StateMachine));

            State nextState = StateMachine.GetState((int)GameState.Reset);
            StateMachine.SetState(nextState);
        }

        public void Awake()
        {
            Object.DontDestroyOnLoad(gameObject);

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        public void OnEnable()
        {
            HookEvents();
        }

        public void OnDisable()
        {
            UnhookEvents();
        }

        protected void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        void Update()
        {
            if (StateMachine != null)
            {
                StateMachine.Update();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJam.Core.State;
using GameJam.Managers._UI.States;

namespace GameJam.Managers {

    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        private FiniteStateMachine stateMachine;

        private static FiniteStateMachine StateMachine
        {
            get { return instance.stateMachine; }
            set { instance.stateMachine = value; }
        }

        public enum UIState
        {
            Reset,
            Load,
            Unload,
            Run,
            Pause
        }

        public struct EventNames
        {

        };

        [SerializeField]
        private UIController m_ui;

        public static UIController UI
        {
            get { return instance.m_ui; }
        }

        private void HookEvents() { }

        private void UnhookEvents() { }

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

        public void Start()
        {
            StateMachine = new FiniteStateMachine();

            StateMachine.AddState((int)UIState.Reset, new Reset(StateMachine));
            StateMachine.AddState((int)UIState.Load, new Load(StateMachine));
            StateMachine.AddState((int)UIState.Run, new Run(StateMachine));
            StateMachine.AddState((int)UIState.Pause, new Pause(StateMachine));

            State nextState = StateMachine.GetState((int)UIState.Reset);
            StateMachine.SetState(nextState);
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

